using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using TCPingInfoView.Utils;
using TCPingInfoView.View;
using TCPingInfoViewLib.SingleInstance;

namespace TCPingInfoView
{
	internal static class App
	{
		private static Window _win;
		private static Application _app;

		[STAThread]
		private static void Main(string[] args)
		{
			Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.GetExecutablePath()));
			var identifier = $@"Global\{UpdateChecker.Name}_{Directory.GetCurrentDirectory().GetDeterministicHashCode()}";
			using var singleInstance = new SingleInstance(identifier);
			if (!singleInstance.IsFirstInstance)
			{
				singleInstance.PassArgumentsToFirstInstance(args.Append(@"--Show"));
				return;
			}
			singleInstance.ArgumentsReceived += SingleInstance_ArgumentsReceived;
			singleInstance.ListenForArgumentsFromSuccessiveInstances();

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

			_app = new Application();
			_app.DispatcherUnhandledException += App_DispatcherUnhandledException;

			_win = new MainWindow();
			_app.MainWindow = _win;
			_win.Show();

			//app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			_app.Run();
		}

		private static void SingleInstance_ArgumentsReceived(object sender, ArgumentsReceivedEventArgs e)
		{
			if (e.Args.Contains(@"--Show"))
			{
				_win?.Dispatcher?.BeginInvoke(() =>
				{
					_win?.ShowWindow();
				});
			}
		}

		private static int _exited;
		private static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			if (Interlocked.Increment(ref _exited) == 1)
			{
				Util.ShowExceptionMessageBox(e.Exception);
				Application.Current.Shutdown();
			}
		}
	}
}
