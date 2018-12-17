using System.Windows.Forms;

namespace TCPingInfoView.Control
{
	[System.ComponentModel.DesignerCategory(@"Code")]
	internal class ToolStripEx : ToolStrip
	{
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x21 && CanFocus && !Focused)
			{
				Focus();
			}

			base.WndProc(ref m);
		}
	}
}
