using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TCPingInfoViewLib.NetUtils
{
	public static class DnsQuery
	{
		public static async Task<IPAddress> GetIpAsync(string host, CancellationToken ct = default, int timeout = 10000)
		{
			try
			{
				var task = Dns.GetHostAddressesAsync(host);
				if (await Task.WhenAny(Task.Delay(timeout, ct), task) == task)
				{
					var ips = await task;
					foreach (var res in ips)
					{
						Debug.WriteLine($@"DNS query {host} answer {res}");
						return res;
					}
				}
				Debug.WriteLine($@"DNS query {host} failed");
				return null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($@"ERROR:{ex.Message}");
				return null;
			}
		}

		public static async Task<string> GetHostNameAsync(IPAddress ip, CancellationToken ct = default, int timeout = 10000)
		{
			try
			{
				var task = Dns.GetHostEntryAsync(ip);
				if (await Task.WhenAny(Task.Delay(timeout, ct), task) == task)
				{
					var res = await task;
					Debug.WriteLine($@"DNS query {ip} answer {res.HostName}");
					return res.HostName;
				}
				Debug.WriteLine($@"DNS query {ip} failed");
				return null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($@"ERROR:{ex.Message}");
				return null;
			}
		}
	}
}
