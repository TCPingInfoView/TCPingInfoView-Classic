using DNS.Client;
using DNS.Client.RequestResolver;
using DNS.Protocol;
using DNS.Protocol.ResourceRecords;
using DNS.Protocol.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPingInfoView.NetUtils
{
	internal class DnsQuery
	{
		private delegate IPHostEntry GetHostEntryHandler(string ip);

		public IPEndPoint[] DnsServers;
		public int Timeout = 2000;

		public DnsQuery(IEnumerable<IPEndPoint> ips)
		{
			DnsServers = ips.ToArray();
		}

		public DnsQuery()
		{
			var localDns = GetLocalDnsAddress();
			DnsServers = IPFormatter.ToIPEndPoints(localDns, 53).ToArray();
		}

		private static bool IsWorkedInterface(NetworkInterface networkInterface)
		{
			if (networkInterface.OperationalStatus == OperationalStatus.Up
				&& (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
					networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
			{
				return true;
			}

			return false;
		}

		public static IEnumerable<IPAddress> GetLocalDnsAddress()
		{
			var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			return from networkInterface in networkInterfaces
				   where IsWorkedInterface(networkInterface)
				   select networkInterface.GetIPProperties()
				   into ipProperties
				   from dns in ipProperties.DnsAddresses
				   where IPFormatter.IsIPv4Address(dns)
				   select dns;
		}

		public static IEnumerable<IPAddress> GetRouteDnsAddress()
		{
			var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			return from networkInterface in networkInterfaces
				   where IsWorkedInterface(networkInterface)
				   select networkInterface.GetIPProperties()
				   into ipProperties
				   from dns in ipProperties.GatewayAddresses
				   where IPFormatter.IsIPv4Address(dns.Address)
				   select dns.Address;
		}

		private IRequest GetRequest(IPEndPoint dns, RecordType type, string queryStr)
		{
			var request = new ClientRequest(dns);
			request.Questions.Add(new Question(Domain.FromString(queryStr), type));
			request.RecursionDesired = true;
			return request;
		}

		private static async Task<IResponse> Query(IPEndPoint dns, IRequest request, int timeout)
		{
			using (var udp = new UdpClient())
			{
				await udp.SendAsync(request.ToArray(), request.Size, dns).WithCancellationTimeout(timeout);

				var result = await udp.ReceiveAsync().WithCancellationTimeout(timeout);

				if (!result.RemoteEndPoint.Equals(dns))
				{
					throw new IOException(@"Remote endpoint mismatch");
				}

				var buffer = result.Buffer;
				var response = Response.FromArray(buffer);

				if (response.Truncated)
				{
					return await new NullRequestResolver().Resolve(request);
				}

				var clientResponse = new ClientResponse(request, response, buffer);
				return clientResponse;
			}
		}

		private static string Response2String(IResponse response, IPEndPoint dns)
		{
			if (response == null)
			{
				var str = $@"*No Response from {dns}";
				Debug.WriteLine(str);
				return string.Empty;
			}

			foreach (var question in response.Questions)
			{
				var records = response.AnswerRecords;
				string str;

				if (records.Count == 0)
				{
					if (question.Type == RecordType.PTR)
					{
						str = $@"*DNS query {IPFormatter.PTRName2IP(question.Name.ToString())} no answer via {dns}";
					}
					else
					{
						str = $@"*DNS query {question.Name} no answer via {dns}";
					}
					Debug.WriteLine(str);
					return string.Empty;
				}
				else
				{
					foreach (var record in records)
					{
						if (record.Type == RecordType.A || record.Type == RecordType.AAAA)
						{
							var ipRecord = (IPAddressResourceRecord)record;
							str = $@"DNS query {question.Name} answer {ipRecord.IPAddress} via {dns}";
							Debug.WriteLine(str);
							if (question.Type == record.Type)
							{
								return ipRecord.IPAddress.ToString();
							}
						}
						else if (record.Type == RecordType.CNAME)
						{
							var cnameRecord = (CanonicalNameResourceRecord)record;
							str = $@"DNS query {question.Name} answer {cnameRecord.CanonicalDomainName} via {dns}";
							Debug.WriteLine(str);
							if (question.Type == record.Type)
							{
								return cnameRecord.CanonicalDomainName.ToString();
							}
						}
						else if (record.Type == RecordType.PTR)
						{
							var ptrRecord = (PointerResourceRecord)record;
							str = $@"DNS query {IPFormatter.PTRName2IP(question.Name.ToString())} answer {ptrRecord.PointerDomainName} via {dns}";
							Debug.WriteLine(str);
							if (question.Type == record.Type)
							{
								return ptrRecord.PointerDomainName.ToString();
							}
						}
						else
						{
							str = $@"DNS query {question.Name} {record.Type} via {dns}";
							Debug.WriteLine(str);
							Console.WriteLine(str);
							throw new NotImplementedException();
						}
					}
				}
			}
			return string.Empty;
		}

		public string Query(string queryStr, RecordType type)
		{
			var origin = queryStr;
			if (RecordType.PTR == type)
			{
				queryStr = IPFormatter.IPStr2PTRName(queryStr);
			}

			if (string.IsNullOrWhiteSpace(queryStr))
			{
				throw new IOException(@"Domain Error!");
			}

			if (DnsServers.Length == 0)
			{
				throw new IOException(@"DNS Server Error!");
			}

			foreach (var dns in DnsServers)
			{
				var isTimeout = false;

				var request = GetRequest(dns, type, queryStr);
				IResponse response = null;
				try
				{
					response = Task.Run(() => Query(dns, request, Timeout)).GetAwaiter().GetResult();
				}
				catch
				{
					isTimeout = true;
				}

				if (!isTimeout)
				{
					var tRes = Response2String(response, dns);
					if (!string.IsNullOrWhiteSpace(tRes))
					{
						return tRes;
					}
				}
			}

			if (RecordType.PTR == type)
			{
				return origin;
			}
			else
			{
				return string.Empty;
			}
		}

		public string Ptr(IPAddress ip)
		{
			return Ptr(ip.ToString());
		}

		public string Ptr(string queryStr)
		{
			return Query(queryStr, RecordType.PTR);
		}

		[Obsolete]
		public static string GetHostName(IPAddress ip, int timeout)
		{
			try
			{
				var callback = new GetHostEntryHandler(Dns.GetHostEntry);
				var result = callback.BeginInvoke(ip.ToString(), null, null);
				if (result.AsyncWaitHandle.WaitOne(timeout, false))
				{
					return callback.EndInvoke(result).HostName;
				}
				else
				{
					return ip.ToString();
				}
			}
			catch (Exception)
			{
				return ip.ToString();
			}
		}

		public IPAddress A(string hostname)
		{
			var ans = Query(hostname, RecordType.A);
			if (string.IsNullOrWhiteSpace(ans))
			{
				return null;
			}

			return IPAddress.Parse(ans);
		}

		public string AAAA(string hostname)
		{
			return Query(hostname, RecordType.AAAA);
		}
	}
}
