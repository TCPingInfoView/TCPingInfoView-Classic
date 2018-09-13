using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

		private int sortIndex = 1;
		protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			base.OnDrawColumnHeader(e);
			var state = e.State == ListViewItemStates.Selected ? VisualStyleElement.Header.Item.Hot : VisualStyleElement.Header.Item.Normal;
			var sortOrder = Sorting == SortOrder.Ascending ? VisualStyleElement.Header.SortArrow.SortedUp : VisualStyleElement.Header.SortArrow.SortedDown;
			var itemRenderer = new VisualStyleRenderer(state);
			var sortRenderer = new VisualStyleRenderer(sortOrder);
			var r = e.Bounds;
			r.X += 1;
			itemRenderer.DrawBackground(e.Graphics, r);
			r.Inflate(-2, 0);
			const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
			itemRenderer.DrawText(e.Graphics, r, e.Header.Text, false, flags);
			var d = SystemInformation.VerticalScrollBarWidth;
			if (e.ColumnIndex == sortIndex)
			{
				sortRenderer.DrawBackground(e.Graphics, new Rectangle(r.Right - d, r.Top, d, r.Height));
			}
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

		protected override void OnColumnClick(ColumnClickEventArgs e)
		{
			base.OnColumnClick(e);
			sortIndex = e.Column;
			ListViewItemSorter = new ListViewItemComparer();
			Sort();
		}
	}

	public class ListViewItemComparer : IComparer
	{
		private int col;

		public int Compare(object x, object y)
		{
			return string.CompareOrdinal(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
		}
	}
}
