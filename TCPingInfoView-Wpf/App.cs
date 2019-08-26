using System;
using System.Net;
using System.Threading;
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
			app.DispatcherUnhandledException += App_DispatcherUnhandledException;

			var win = new MainWindow();
			app.MainWindow = win;
			win.Show();

			//app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			app.Run();
		}

		private static int _exited;
		private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			if (Interlocked.Increment(ref _exited) == 1)
			{
				MessageBox.Show(e.Exception.Message, UpdateChecker.Name, MessageBoxButton.OK, MessageBoxImage.Error);
				Application.Current.Shutdown();
			}
		}
	}
}
