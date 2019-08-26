using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TCPingInfoView.Utils
{
	public static class I18NUtil
	{
		private const string DefaultLanguage = @"en-US";

		public static string CurrentLanguage;

		public static readonly Dictionary<string, string> SupportLanguage = new Dictionary<string, string>
		{
			{@"简体中文", @"zh-CN"},
			{@"English (United States)", @"en-US"},
		};

		public static string GetLanguage(string name)
		{
			return SupportLanguage.All(s => name != s.Value) ? GetLanguage() : name;
		}

		public static string GetLanguage()
		{
			var name = System.Globalization.CultureInfo.CurrentCulture.Name;
			return SupportLanguage.Any(s => name == s.Value) ? name : DefaultLanguage;
		}

		public static void SetLanguage(string langName)
		{
			SetLanguage(Application.Current.Resources, @"App", langName);
			CurrentLanguage = langName;
		}

		public static string GetAppStringValue(string key)
		{
			if (Application.Current.Resources.MergedDictionaries[0][key] is string str)
			{
				return str;
			}
			return null;
		}

		public static string GetWindowStringValue(Window window, string key)
		{
			if (window.Resources.MergedDictionaries[0][key] is string str)
			{
				return str;
			}
			return null;
		}

		public static void SetLanguage(ResourceDictionary resources, string filename, string langName = @"")
		{
			if (string.IsNullOrEmpty(langName))
			{
				langName = GetLanguage();
			}
			if (Application.LoadComponent(new Uri($@"../I18N/{filename}.{langName}.xaml", UriKind.Relative)) is ResourceDictionary langRd)
			{
				resources.MergedDictionaries.Add(langRd);
				while (resources.MergedDictionaries.Count > 1)
				{
					resources.MergedDictionaries.RemoveAt(0);
				}
			}
		}
	}
}
