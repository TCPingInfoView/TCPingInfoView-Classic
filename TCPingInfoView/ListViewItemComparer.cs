using System;
using System.Collections;
using System.Windows.Forms;

namespace TCPingInfoView
{
	public class ListViewItemComparer : IComparer
	{
		private readonly int ColumnToSort;
		private readonly SortOrder OrderOfSort;
		private readonly Type Type;

		public ListViewItemComparer(int column, SortOrder orderOfSort, Type type)
		{
			OrderOfSort = orderOfSort;
			Type = type;
			ColumnToSort = column;
		}

		public int Compare(object x, object y)
		{
			int res;
			var a = ((ListViewItem)x).SubItems[ColumnToSort].Text;
			var b = ((ListViewItem)y).SubItems[ColumnToSort].Text;
			if (string.IsNullOrWhiteSpace(a) && string.IsNullOrWhiteSpace(b))
			{
				return 0;
			}
			if (string.IsNullOrWhiteSpace(a))
			{
				return -1;
			}
			if (string.IsNullOrWhiteSpace(b))
			{
				return 1;
			}
			if (Type == typeof(int))
			{
				res = Convert.ToInt32(a) - Convert.ToInt32(b);
			}
			else if (Type == typeof(double))
			{
				var d1 = 1.0;
				var d2 = 1.0;
				while (a.EndsWith("%"))
				{
					d1 *= 100;
					a = a.Remove(a.Length - 1);
				}

				while (b.EndsWith("%"))
				{
					d2 *= 100;
					b = b.Remove(b.Length - 1);
				}
				res = Convert.ToInt32(Convert.ToDouble(a) * d1 - Convert.ToDouble(b) * d2);
			}
			else
			{
				res = string.CompareOrdinal(a, b);
			}
			if (OrderOfSort == SortOrder.Ascending)
			{
				return res;
			}
			else
			{
				return -res;
			}
		}
	}
}
