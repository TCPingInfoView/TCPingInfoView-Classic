using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TCPingInfoView
{
	public static class Util
	{
		private static readonly Regex Ipv4Pattern = new Regex("^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){1}(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){2}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");

		public static bool IsIPv4Address(string input)
		{
			return Ipv4Pattern.IsMatch(input);
		}

		public static bool IsPort(int port)
		{
			if (port >= IPEndPoint.MinPort + 1 && port <= IPEndPoint.MaxPort)
			{
				return true;
			}

			return false;
		}

		public static async Task<IPEndPoint> ToIPEndPoint(string str, int defaultport)
		{
			if (string.IsNullOrWhiteSpace(str) || !IsPort(defaultport))
			{
				return null;
			}

			var s = str.Split(':');
			if (s.Length == 1 || s.Length == 2)
			{
				var ips = await Dns.GetHostAddressesAsync(s[0]);
				if (ips.Length == 0)
				{
					return null;
				}
				var ip = ips[0];
				if (s.Length == 2)
				{
					var port = Convert.ToInt32(s[1]);
					if (IsPort(port))
					{
						return new IPEndPoint(ip, port);
					}
				}
				else
				{
					return new IPEndPoint(ip, defaultport);
				}
			}

			return null;
		}

		public static async Task<Data> Stringline2Data(string line)
		{
			var s = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length < 1)
			{
				return null;
			}

			var ipport = await ToIPEndPoint(s[0], 443);
			if (ipport == null)
			{
				return null;
			}

			var hostname = s[0].Split(':')[0];

			var res = new Data
			{
				HostsName = hostname,
				IpPort = ipport,
				Description = string.Empty
			};

			if (s.Length == 2)
			{
				res.Description = s[1];
			}

			return res;
		}

		public static async Task<IEnumerable<Data>> ToData(IEnumerable<string> sl)
		{
			var data = new List<Data>();
			foreach (var line in sl)
			{
				var l = await Stringline2Data(line);
				if (l != null)
				{
					data.Add(l);
				}
			}
			return data;
		}
	}
}
