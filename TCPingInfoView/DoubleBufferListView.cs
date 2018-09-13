using System.Drawing;
using System.Windows.Forms;

namespace TCPingInfoView
{
	[System.ComponentModel.DesignerCategory(@"Code")]
	public class DoubleBufferListView : ListView
	{
		public DoubleBufferListView()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			UpdateStyles();
			OwnerDraw = true;
		}

		protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			base.OnDrawColumnHeader(e);
			e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
			e.Graphics.DrawRectangle(SystemPens.GradientInactiveCaption, new Rectangle(e.Bounds.X, -1, e.Bounds.Width, e.Bounds.Height));
			var text = Columns[e.ColumnIndex].Text;
			const TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
			TextRenderer.DrawText(e.Graphics, text, Font, e.Bounds, Color.Black, cFlag);
		}

		protected override void OnDrawItem(DrawListViewItemEventArgs e)
		{
			base.OnDrawItem(e);
			e.DrawDefault = true;
		}

		protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			base.OnDrawSubItem(e);
			e.DrawDefault = true;
		}
	}
}
