using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TCPingInfoView.Forms;

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
							$@"{ExeName} 已经在运行！" + Environment.NewLine +
							$@"请在任务栏里寻找 {ExeName} 图标。" + Environment.NewLine +
							@"如果想启动多份，建议另外复制一份到别的目录。",
							$@"{ExeName} 已经在运行", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
	}
}
