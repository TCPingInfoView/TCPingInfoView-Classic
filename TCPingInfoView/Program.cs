using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
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
			using (var mutex = new Mutex(false, @"Global\TCPingInfoView_" + Application.StartupPath.GetHashCode()))
			{
				if (!mutex.WaitOne(0, false))
				{
					var dr = MessageBox.Show(
@"TCPingInfoView 已经在运行！" + Environment.NewLine +
@"请在任务栏里寻找 TCPingInfoView 图标。" + Environment.NewLine +
@"如果想启动多份，建议另外复制一份到别的目录。" + Environment.NewLine +
@"你确定一定要再次运行吗？", @"TCPingInfoView 已经在运行", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						return;
					}
				}

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
}
