using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TCPingInfoView.Utils
{
	public static class I18NUtil
	{
		private const string DefaultLanguage = @"en-US";
		public static readonly Dictionary<string, string> SupportLanguage = new Dictionary<string, string>
		{
			{@"简体中文", @"zh-CN"},
			{@"English (United States)", @"en-US"},
		};

		public static string GetLanguage()
		{
			var name = System.Globalization.CultureInfo.CurrentCulture.Name;
			return SupportLanguage.Any(s => name == s.Value) ? name : DefaultLanguage;
		}

		public static void SetLanguage(string langName)
		{
			App.SetLanguage(langName);
		}

		public static string GetAppStringValue(string key)
		{
			if (Application.Current.Resources.MergedDictionaries[0][key] is string str)
			{
				return str;
			}
			return null;
		}
	}
}
