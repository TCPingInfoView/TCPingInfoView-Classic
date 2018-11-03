using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TCPingInfoView.Steamworks;
using TCPingInfoView.Forms;

namespace TCPingInfoView
{
	static class Program
	{
		[DllImport(@"user32.dll")]
		private static extern bool SetProcessDPIAware();

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			SteamManager.Init();

			if (Environment.OSVersion.Version.Major >= 6)
			{
				SetProcessDPIAware();
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
