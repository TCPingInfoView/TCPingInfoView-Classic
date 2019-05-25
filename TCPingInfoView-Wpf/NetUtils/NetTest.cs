using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPingInfoView.Model;

namespace TCPingInfoView.NetUtils
{
	public static class NetTest
	{
		public static async Task<double?> TCPingAsync(IPAddress ip, int port = 80, int timeout = 1000)
		{
			return await TCPingAsync(ip, new CancellationTokenSource(), port, timeout);
		}

		public static async Task<double?> TCPingAsync(IPAddress ip, CancellationTokenSource cts, int port = 80, int timeout = 1000)
		{
			if (ip == null)
			{
				return null;
			}

			using var client = new TcpClient(ip.AddressFamily);

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			await Task.WhenAny(Task.Delay(timeout, cts.Token), client.ConnectAsync(ip, port));

			stopwatch.Stop();
			var t = stopwatch.Elapsed.TotalMilliseconds;

			if (client.Connected == false)
			{
				Debug.WriteLine($@"TCPing [{ip}]:{port}:超时({t}ms > {timeout}ms)");
				return null;
			}

			Debug.WriteLine($@"TCPing [{ip}]:{port}:{t:0.00}ms");
			return t;
		}

		public static double? TCPing(IPAddress ip, int port = 80, int timeout = 1000)
		{
			if (ip == null)
			{
				return null;
			}

			using var client = new TcpClient(ip.AddressFamily);

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			client.ConnectAsync(ip, port).Wait(timeout);

			stopwatch.Stop();
			var t = stopwatch.Elapsed.TotalMilliseconds;

			if (client.Connected == false)
			{
				Debug.WriteLine($@"TCPing [{ip}]:{port}:超时({t}ms > {timeout}ms)");
				return null;
			}

			Debug.WriteLine($@"TCPing [{ip}]:{port}:{t:0.00}ms");
			return t;
		}

		public static async Task<ICMPingStatus> ICMPingAsync(IPAddress ip, int timeout = 1000)
		{
			return await ICMPingAsync(ip, new CancellationTokenSource(), timeout);
		}

		public static async Task<ICMPingStatus> ICMPingAsync(IPAddress ip, CancellationTokenSource cts, int timeout = 1000)
		{
			var res = new ICMPingStatus();
			if (ip == null)
			{
				return null;
			}

			var p1 = new Ping();

			var task = p1.SendPingAsync(ip, timeout);

			if (await Task.WhenAny(Task.Delay(int.MaxValue, cts.Token), task) == task)
			{
				var reply = await task;
				if (reply != null && reply.Status == IPStatus.Success)
				{
					res.Status = reply.Status;
					res.Address = reply.Address;
					res.RTT = reply.RoundtripTime;
					res.TTL = reply.Options?.Ttl;
					res.bytes = reply.Buffer.Length;
#if DEBUG
					//Debug info
					var sb = new StringBuilder();
					sb.AppendLine($@"Status: {res.Status}");
					sb.AppendLine($@"Address: {res.Address}");
					sb.AppendLine($@"RTT: {res.RTT}");
					sb.AppendLine($@"TTL: {res.TTL}");
					sb.AppendLine($@"Buffer size: {res.bytes}");
					Debug.WriteLine(sb.ToString());
#endif
				}
				else if (reply != null && reply.Status == IPStatus.TimedOut)
				{
					Debug.WriteLine($@"ICMPing {ip} Timeout");
					res.Status = reply.Status;
				}
				else
				{
					Debug.WriteLine($@"ICMPing {ip} 失败");
					res.Status = IPStatus.Unknown;
				}
				return res;
			}
			else
			{
				Debug.WriteLine($@"ICMPing {ip} Task was cancelled");

				return null;
			}
		}
	}
}