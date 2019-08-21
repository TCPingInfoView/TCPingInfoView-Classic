using System;
using System.Net;
using System.Windows;
using TCPingInfoView.Utils;
using TCPingInfoView.View;

namespace TCPingInfoView
{
	internal static class App
	{
		[STAThread]
		private static void Main()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

			var app = new Application();
			SetLanguage();

			var win = new MainWindow();
			app.MainWindow = win;
			win.Show();

			//app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			app.Run();
		}

		public static void SetLanguage(string langName = @"")
		{
			if (string.IsNullOrEmpty(langName))
			{
				langName = I18NUtil.GetLanguage();
			}

			if (Application.LoadComponent(new Uri($@"../I18N/App.{langName}.xaml", UriKind.Relative)) is ResourceDictionary langRd)
			{
				Application.Current.Resources.MergedDictionaries.Add(langRd);
				if (Application.Current.Resources.MergedDictionaries.Count > 1)
				{
					Application.Current.Resources.MergedDictionaries.RemoveAt(0);
				}
			}
		}
	}
}
