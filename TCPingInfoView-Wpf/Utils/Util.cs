using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TCPingInfoView.ViewModel;
using TCPingInfoViewLib.NetUtils;

namespace TCPingInfoView.Utils
{
	public static class Util
	{
		#region Data

		public static readonly UTF8Encoding Utf8WithoutBom = new UTF8Encoding(false);

		public const string ConfigFileName = @"TCPingInfoView.json";

		public static readonly string CurrentDirectory = Path.GetDirectoryName(GetExecutablePath());

		#endregion

		public static EndPointInfo StringLine2Data(string line, int index)
		{
			var s = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length < 1)
			{
				return null;
			}

			string hostname = null;
			IPAddress ip;
			ushort port = 443;

			var sp = IPFormatter.EndPointRegexStr.Match(s[0]).Groups;
			if (sp.Count == 5)
			{
				var temp = string.IsNullOrWhiteSpace(sp[1].Value) ? sp[3].Value : sp[1].Value;
				if (!IPAddress.TryParse(temp, out ip))
				{
					hostname = temp;
				}

				if (!ushort.TryParse(string.IsNullOrWhiteSpace(sp[2].Value) ? sp[4].Value : sp[2].Value, out port))
				{
					return null;
				}
			}
			else if (sp.Count == 1)
			{
				var groups = Regex.Match(s[0], @"^\[(.*)\]$").Groups;
				if (groups.Count == 2)
				{
					var temp = groups[1].Value;
					if (!IPAddress.TryParse(temp, out ip))
					{
						hostname = temp;
					}
				}
				else
				{
					var temp = s[0];
					if (!IPAddress.TryParse(temp, out ip))
					{
						hostname = temp;
					}
				}
			}
			else
			{
				return null;
			}

			var res = new EndPointInfo(index)
			{
				Hostname = hostname,
				Ip = ip,
				Port = port,
				Description = string.Empty,
				IsRememberIp = ip != null
			};

			if (s.Length == 2)
			{
				res.Description = s[1];
			}

			return res;
		}

		public static IEnumerable<EndPointInfo> ToEndPoints(IEnumerable<string> sl)
		{
			var i = 0;
			var res = new List<EndPointInfo>();
			foreach (var line in sl)
			{
				++i;
				res.Add(StringLine2Data(line, i));
			}
			return res;
		}

		public static string GetExecutablePath()
		{
			var p = Process.GetCurrentProcess();
			if (p.MainModule != null)
			{
				var res = p.MainModule.FileName;
				return res;
			}

			var dllPath = GetDllPath();
			return Path.Combine(Path.GetDirectoryName(dllPath) ?? throw new InvalidOperationException(),
					$@"{Path.GetFileNameWithoutExtension(dllPath)}.exe");
		}

		public static string GetDllPath()
		{
			return Assembly.GetExecutingAssembly().Location;
		}

	}
}
