using System;
using System.Windows.Forms;
using TCPingInfoView.Collection;
using TCPingInfoView.I18n;

namespace TCPingInfoView.Forms
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

		private int GetWidth()
		{
			var i = 0;
			var j = 0;
			i = label1.Width > i ? label1.Width : i;
			i = label2.Width > i ? label2.Width : i;
			i = label3.Width > i ? label3.Width : i;
			i = label4.Width > i ? label4.Width : i;
			i = label5.Width > i ? label5.Width : i;
			i = label6.Width > i ? label6.Width : i;
			i = label7.Width > i ? label7.Width : i;
			i = label8.Width > i ? label8.Width : i;
			i = label9.Width > i ? label9.Width : i;
			i = label10.Width > i ? label10.Width : i;
			i = label11.Width > i ? label11.Width : i;
			i = label12.Width > i ? label12.Width : i;

			j = textBox1.Width > j ? textBox1.Width : j;
			j = textBox2.Width > j ? textBox2.Width : j;
			j = textBox3.Width > j ? textBox3.Width : j;
			j = textBox4.Width > j ? textBox4.Width : j;
			j = textBox5.Width > j ? textBox5.Width : j;
			j = textBox6.Width > j ? textBox6.Width : j;
			j = textBox7.Width > j ? textBox7.Width : j;
			j = textBox8.Width > j ? textBox8.Width : j;
			j = textBox9.Width > j ? textBox9.Width : j;
			j = textBox10.Width > j ? textBox10.Width : j;
			j = textBox11.Width > j ? textBox11.Width : j;
			j = textBox12.Width > j ? textBox12.Width : j;

			return i + j + 43;
		}

		private void LoadI18N()
		{
			Text = I18N.GetString(@"Properties");
			button1.Text = I18N.GetString(@"OK");
			label1.Text = I18N.GetString(@"Host Name:");
			label2.Text = I18N.GetString(@"IP:Port:");
			label3.Text = I18N.GetString(@"Succeed Count:");
			label4.Text = I18N.GetString(@"Failed Count:");
			label5.Text = I18N.GetString(@"% Succeed:");
			label6.Text = I18N.GetString(@"% Failed:");
			label7.Text = I18N.GetString(@"Description:");
			label8.Text = I18N.GetString(@"Last TCPing Latency:");
			label9.Text = I18N.GetString(@"Average TCPing Latency:");
			label10.Text = I18N.GetString(@"Max TCPing Latency:");
			label11.Text = I18N.GetString(@"Min TCPing Latency:");
			label12.Text = I18N.GetString(@"Order:");
			Width = GetWidth();
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			LoadI18N();
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
