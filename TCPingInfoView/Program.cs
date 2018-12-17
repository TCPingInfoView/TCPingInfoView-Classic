using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TCPingInfoView.Forms;
using TCPingInfoView.I18n;
using TCPingInfoView.Steamworks;

namespace TCPingInfoView
{
	internal static class Program
	{
		private static string ExeName => Assembly.GetExecutingAssembly().GetName().Name;

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		private static void Main()
		{
			using (var mutex = new Mutex(false, $@"Global\{ExeName}_" + Application.StartupPath.GetHashCode()))
			{
				if (!mutex.WaitOne(0, false))
				{
					MessageBox.Show(
							string.Format(I18N.GetString(@"{0} is already running!"), ExeName) + Environment.NewLine +
							string.Format(I18N.GetString(@"Find {0} icon in your notify tray."), ExeName) + Environment.NewLine +
							I18N.GetString(@"If you want to start more instances, make a copy in another directory."),
							string.Format(I18N.GetString(@"{0} is already running!"), ExeName), MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				SteamManager.Init();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
	}
}
