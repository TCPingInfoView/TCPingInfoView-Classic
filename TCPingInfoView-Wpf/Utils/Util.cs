using System;
using System.Net;
using System.Text.RegularExpressions;
using TCPingInfoView.Model;
using TCPingInfoView.NetUtils;

namespace TCPingInfoView.Utils
{
	public static class Util
	{
		public static EndPointInfo StringLine2Data(string line)
		{
			var s = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length < 1)
			{
				return null;
			}

			string hostname;
			IPAddress ip;
			var port = 443;

			var sp = IPFormatter.EndPointRegexStr.Match(s[0]).Groups;
			if (sp.Count == 5)
			{
				hostname = string.IsNullOrWhiteSpace(sp[1].Value) ? sp[3].Value : sp[1].Value;
				IPAddress.TryParse(hostname, out ip);

				if (!int.TryParse(string.IsNullOrWhiteSpace(sp[2].Value) ? sp[4].Value : sp[2].Value, out port))
				{
					return null;
				}

				if (!IPFormatter.IsPort(port))
				{
					return null;
				}
			}
			else if (sp.Count == 1)
			{
				var groups = Regex.Match(s[0], @"^\[(.*)\]$").Groups;
				if (groups.Count == 2)
				{
					hostname = groups[1].Value;
					IPAddress.TryParse(hostname, out ip);
				}
				else
				{
					hostname = s[0];
					IPAddress.TryParse(hostname, out ip);
				}
			}
			else
			{
				return null;
			}

			var res = new EndPointInfo
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
	}
}
