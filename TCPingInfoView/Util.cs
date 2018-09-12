using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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

		public static IPEndPoint ToIPEndPoint(string str, int defaultport)
		{
			if (string.IsNullOrWhiteSpace(str) || !IsPort(defaultport))
			{
				return null;
			}

			var s = str.Split(':');
			if (s.Length == 1 || s.Length == 2)
			{
				var ip = IPAddress.Parse(s[0]);
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

		public static async Task<IPEndPoint> ToIPEndPointAsync(string str, int defaultport)
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

		public static Data Stringline2Data(string line)
		{
			var s = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length < 1)
			{
				return null;
			}

			string hostname;
			IPAddress ip = null;
			var port = 443;

			var sp = s[0].Split(':');
			if (sp.Length == 1 || sp.Length == 2)
			{
				hostname = sp[0];
				if (IsIPv4Address(hostname))
				{
					ip = IPAddress.Parse(hostname);
				}
			}
			else
			{
				return null;
			}
			if (sp.Length == 2)
			{
				try
				{
					port = Convert.ToInt32(sp[1]);
				}
				catch
				{
					return null;
				}

				if (!IsPort(port))
				{
					return null;
				}
			}


			var res = new Data
			{
				HostsName = hostname,
				Ip = ip,
				Port = port,
				Description = string.Empty
			};

			if (s.Length == 2)
			{
				res.Description = s[1];
			}

			return res;
		}

		public static IEnumerable<Data> ToData(IEnumerable<string> sl)
		{
			var data = new List<Data>();
			foreach (var line in sl)
			{
				var l = Stringline2Data(line);
				if (l != null)
				{
					data.Add(l);
				}
			}
			return data;
		}

		public static void Invoke(this Control control, MethodInvoker action)
		{
			control.Invoke(action);
		}
	}
}
