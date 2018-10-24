using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace TCPingInfoView.NetUtils
{
	public static class IPFormatter
	{
		private static readonly Regex Ipv4Pattern = new Regex("^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){1}(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){2}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");

		public static bool IsIPv4Address(string input)
		{
			return Ipv4Pattern.IsMatch(input);
		}

		public static bool IsIPv4Address(IPAddress ipAddress)
		{
			return ipAddress.AddressFamily == AddressFamily.InterNetwork;
		}

		public static bool IsIPv6Address(IPAddress ipAddress)
		{
			return ipAddress.AddressFamily == AddressFamily.InterNetworkV6;
		}

		public static bool IsPort(int port)
		{
			if (port >= IPEndPoint.MinPort && port <= IPEndPoint.MaxPort)
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
				if (!IsIPv4Address(s[0]))
				{
					return null;
				}

				var ip = IPAddress.Parse(s[0]);
				if (s.Length == 2)
				{
					var port = Convert.ToInt32(s[1]);
					if (IsPort(port))
					{
						return ToIPEndPoint(ip, port);
					}
				}
				else
				{
					return ToIPEndPoint(ip, defaultport);
				}
			}

			return null;
		}

		public static IPEndPoint ToIPEndPoint(IPAddress ip, int port)
		{
			return new IPEndPoint(ip, port);
		}

		public static IEnumerable<IPEndPoint> ToIPEndPoints(string str, int defaultport, char[] separator)
		{
			var s = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			return s.Select(ipEndPointsStr => ToIPEndPoint(ipEndPointsStr, defaultport)).Where(ipend => ipend != null);
		}

		public static IEnumerable<IPEndPoint> ToIPEndPoints(IEnumerable<IPAddress> ips, int port)
		{
			return ips.Select(ip => ToIPEndPoint(ip, port));
		}

		public static IPAddress PTRName2IP(string str)
		{
			var s = str.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			return IPAddress.Parse($@"{s[3]}.{s[2]}.{s[1]}.{s[0]}");
		}

		public static string IPStr2PTRName(string str)
		{
			if (!IsIPv4Address(str))
			{
				return string.Empty;
			}

			var s = str.Split('.');
			return $@"{s[3]}.{s[2]}.{s[1]}.{s[0]}.in-addr.arpa";
		}
	}
}