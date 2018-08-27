using System.Diagnostics;
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
			BackgroundColor = DefaultCellStyle.BackColor;
		}

		protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
		{
			base.PaintBackground(graphics, clipBounds, gridBounds);

			var rowHeight = RowTemplate.Height;
			var h = ColumnHeadersHeight + rowHeight * RowCount;
			var imgWidth = Width - 2;
			var pen = new Pen(GridColor, 1);
			var rowImg = new Bitmap(imgWidth, rowHeight);
			var g = Graphics.FromImage(rowImg);

			var w = 0;
			if (RowHeadersVisible)
			{
				w = RowHeadersWidth - 1;
			}

			for (var j = 0; j < ColumnCount; ++j)
			{
				g.DrawLine(pen, new Point(w, 0), new Point(w, 2 * rowHeight));
				w += Columns[j].Width;
			}

			g.DrawLine(pen, new Point(0, 0), new Point(imgWidth, 0));

			if (Height <= h)
			{
				Debug.WriteLine($@"{clipBounds.X} {clipBounds.Y} {clipBounds.Width} {clipBounds.Height}");
				Debug.WriteLine($@"{gridBounds.X} {gridBounds.Y} {gridBounds.Width} {gridBounds.Height}");
				Debug.WriteLine($@"{h}");
				Debug.WriteLine($@"{Height}");

				var loop = (Height - clipBounds.Y) / rowHeight;
				for (var j = 0; j < loop + 1; ++j)
				{
					graphics.DrawImage(rowImg, gridBounds.X, clipBounds.Y + j * rowHeight);
				}
			}
			else
			{
				var loop = (Height - h) / rowHeight;
				for (var j = 0; j < loop + 1; ++j)
				{
					graphics.DrawImage(rowImg, gridBounds.X, h + j * rowHeight);
				}
			}
		}
	}
}
