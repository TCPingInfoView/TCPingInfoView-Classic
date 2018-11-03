using System;
using System.Windows.Forms;

namespace TCPingInfoView.Forms
{
	public partial class DisplayedColumns : Form
	{
		public DisplayedColumns(DataGridViewColumnCollection columns)
		{
			InitializeComponent();
			Columns = columns;
		}

		private readonly DataGridViewColumnCollection Columns;

		private void DisplayedColumns_Load(object sender, EventArgs e)
		{
			foreach (DataGridViewColumn column in Columns)
			{
				checkedListBox1.Items.Add(column.HeaderText);
			}

			for (var i = 0; i < Columns.Count; ++i)
			{
				checkedListBox1.SetItemChecked(i, Columns[i].Visible);
			}
		}

		private void OK_button_Click(object sender, EventArgs e)
		{
			for (var i = 1; i < Columns.Count; ++i)
			{
				Columns[i].Visible = checkedListBox1.GetItemChecked(i);
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void Cancel_button_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.Index == 0)
			{
				e.NewValue = CheckState.Checked;
			}
		}
	}
}
