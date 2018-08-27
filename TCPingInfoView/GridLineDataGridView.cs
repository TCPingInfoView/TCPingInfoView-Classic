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
			var rFrame = new Rectangle(0, 0, imgWidth, rowHeight);
			var rFill = new Rectangle(1, 1, imgWidth - 2, rowHeight);
			var pen = new Pen(GridColor, 1);
			var rowImg = new Bitmap(imgWidth, rowHeight);
			var g = Graphics.FromImage(rowImg);
			g.DrawRectangle(pen, rFrame);
			g.FillRectangle(new SolidBrush(Color.Transparent), rFill);
			var w = 0;
			if (RowHeadersVisible)
			{
				var rowHeader = new Rectangle(2, 2, RowHeadersWidth - 3, rowHeight);
				g.FillRectangle(new SolidBrush(RowHeadersDefaultCellStyle.BackColor), rowHeader);
				w = RowHeadersWidth - 1;
			}

			for (var j = 0; j < ColumnCount; ++j)
			{
				g.DrawLine(pen, new Point(w, 0), new Point(w, 2 * rowHeight));
				w += Columns[j].Width;
			}


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
