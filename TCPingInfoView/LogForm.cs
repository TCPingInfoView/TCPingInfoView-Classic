using System;
using System.Windows.Forms;
using TCPingInfoView.Collection;

namespace TCPingInfoView
{
	public partial class LogForm : Form
	{
		public LogForm(MainTable table)
		{
			InitializeComponent();
			Table = table;
		}

		private readonly MainTable Table;

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			textBox1.Text = Table.HostsName;
			textBox2.Text = Table.Endpoint;
			textBox3.Text = $@"{Table.SucceedCount}";
			textBox4.Text = $@"{Table.FailedCount}";
			textBox5.Text = Table.SucceedP;
			textBox6.Text = Table.FailedP;
			textBox7.Text = Table.Description;
			textBox8.Text = $@"{Table.LastPing}";
			textBox9.Text = $@"{Table.Average}";
			textBox10.Text = $@"{Table.MaxPing}";
			textBox11.Text = $@"{Table.MinPing}";
			textBox12.Text = $@"{Table.Index}";
		}
	}
}
