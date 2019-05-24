using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TCPingInfoView.NetUtils
{
	public static class DnsQuery
	{
		public static async Task<IPAddress> GetIpAsync(string host, int timeout = 10000)
		{
			return await GetIpAsync(host, new CancellationTokenSource(), timeout);
		}

		public static async Task<IPAddress> GetIpAsync(string host, CancellationTokenSource cts, int timeout = 10000)
		{
			try
			{
				var task = Dns.GetHostAddressesAsync(host);
				if (await Task.WhenAny(task, Task.Delay(timeout, cts.Token)) == task)
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

		private delegate IPHostEntry GetHostEntryHandler(string ip);

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

		[Obsolete]
		public static async Task<string> GetHostNameAsync(IPAddress ip)
		{
			var res = ip.ToString();
			await Task.Run(() => { res = Dns.Resolve(ip.ToString()).HostName; });
			return res;
		}

	}
}
