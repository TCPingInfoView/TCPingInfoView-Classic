using System.Drawing;
using System.Windows.Forms;

namespace TCPingInfoView
{
	public class GridLineDataGridView : DataGridView
	{
		public GridLineDataGridView()
		{
			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			CellBorderStyle = DataGridViewCellBorderStyle.Single;
			//RowHeadersVisible = false;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var rowHeight = RowTemplate.Height;
			var h = ColumnHeadersHeight + rowHeight * RowCount;
			var imgWidth = Width - 2;
			var rFrame = new Rectangle(0, 0, imgWidth, rowHeight);
			var rFill = new Rectangle(1, 1, imgWidth - 2, rowHeight);
			var pen = new Pen(GridColor, 1);
			var rowImg = new Bitmap(imgWidth, rowHeight);
			var g = Graphics.FromImage(rowImg);
			g.DrawRectangle(pen, rFrame);
			g.FillRectangle(new SolidBrush(DefaultCellStyle.BackColor), rFill);
			var w = - 1;
			if (RowHeadersVisible)
			{
				var rowHeader = new Rectangle(2, 2, RowHeadersWidth - 3, rowHeight);
				g.FillRectangle(new SolidBrush(RowHeadersDefaultCellStyle.BackColor), rowHeader);
				w = RowHeadersWidth - 1;
			}
			for (var j = 0; j < ColumnCount; ++j)
			{
				g.DrawLine(pen, new Point(w, 0), new Point(w, rowHeight));
				w += Columns[j].Width;
			}
			var loop = (Height - h) / rowHeight;
			for (var j = 0; j < loop + 1; ++j)
			{
				e.Graphics.DrawImage(rowImg, 1, h + j * rowHeight);
			}
		}
	}
}
