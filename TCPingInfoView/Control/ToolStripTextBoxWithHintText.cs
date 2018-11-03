using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TCPingInfoView.Control
{
	[System.ComponentModel.DesignerCategory(@"Code")]
	public class ToolStripTextBoxWithHintText : ToolStripTextBox
	{
		private const int EM_SETCUEBANNER = 0x1501;

		[DllImport(@"user32.dll", CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

		public ToolStripTextBoxWithHintText()
		{
			Control.HandleCreated += Control_HandleCreated;
		}

		private void Control_HandleCreated(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(cueBanner))
			{
				UpdateCueBanner();
			}
		}

		private string cueBanner;

		public string CueBanner
		{
			get => cueBanner;
			set
			{
				cueBanner = value;
				UpdateCueBanner();
			}
		}

		private void UpdateCueBanner()
		{
			SendMessage(Control.Handle, EM_SETCUEBANNER, 0, cueBanner);
		}
	}
}