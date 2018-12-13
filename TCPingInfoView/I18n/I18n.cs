using System.Collections.Generic;
using System.Text.RegularExpressions;
using TCPingInfoView.Properties;

namespace TCPingInfoView.I18n
{
	public static class I18N
	{
		private static readonly Dictionary<string, string> Strings;

		private static void Init(string res)
		{
			var lines = Regex.Split(res, "\r\n|\r|\n");
			foreach (var line in lines)
			{
				if (line.StartsWith("#"))
				{
					continue;
				}

				var kv = Regex.Split(line, @"=");
				if (kv.Length == 2)
				{
					var val = Regex.Replace(kv[1], "\\\\n", "\r\n");
					Strings[kv[0]] = val;
				}
			}
		}

		static I18N()
		{
			Strings = new Dictionary<string, string>();

			var name = System.Globalization.CultureInfo.CurrentCulture.Name;
			if (name == @"zh" || name == @"zh-CN")
			{
				Init(Resources.zh_CN);
			}
		}

		public static string GetString(string key)
		{
			return Strings.TryGetValue(key, out var value) ? value : key;
		}
	}
}
