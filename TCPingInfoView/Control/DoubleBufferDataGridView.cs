using System.Drawing;
using System.Windows.Forms;

namespace TCPingInfoView.Control
{
	[System.ComponentModel.DesignerCategory(@"Code")]
	public class DoubleBufferDataGridView : DataGridView
	{
		public DoubleBufferDataGridView()
		{
			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			CellBorderStyle = DataGridViewCellBorderStyle.Single;
			BackgroundColor = DefaultCellStyle.BackColor;
			MultiSelect = false;
			SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			UpdateStyles();
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
				g.DrawLine(pen, new Point(w, 0), new Point(w, rowHeight));
				w += Columns[j].Width;
			}

			if (Height <= h)
			{
				//Debug.WriteLine($@"{clipBounds.X} {clipBounds.Y} {clipBounds.Width} {clipBounds.Height}");
				//Debug.WriteLine($@"{gridBounds.X} {gridBounds.Y} {gridBounds.Width} {gridBounds.Height}");
				//Debug.WriteLine($@"{h}");
				//Debug.WriteLine($@"{Height}");

				var loop = (Height - clipBounds.Y) / rowHeight;
				for (var j = 0; j < loop + 1; ++j)
				{
					graphics.DrawImage(rowImg, gridBounds.X, clipBounds.Y + j * rowHeight);
				}
			}
			else
			{
				g.DrawLine(pen, new Point(0, rowHeight - 1), new Point(imgWidth, rowHeight - 1));
				var loop = (Height - h) / rowHeight;
				for (var j = 0; j < loop + 1; ++j)
				{
					graphics.DrawImage(rowImg, gridBounds.X, h + j * rowHeight);
				}
			}
		}

		protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			base.OnCellMouseDown(e);
			if (e.RowIndex >= 0)
			{
				Rows[e.RowIndex].Selected = true;
			}
		}

		protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
		{
			base.OnColumnHeaderMouseClick(e);
			if (SelectedCells.Count > 0)
			{
				Rows[SelectedCells[0].RowIndex].Selected = true;
			}
		}
	}
}
