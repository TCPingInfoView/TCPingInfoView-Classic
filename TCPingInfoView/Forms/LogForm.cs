using System;
using System.Windows.Forms;
using TCPingInfoView.Collection;
using TCPingInfoView.I18n;
using TCPingInfoView.Model;
using TCPingInfoView.Util;

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
			foreach (System.Windows.Forms.Control control in Controls)
			{
				if (control is Label)
				{
					var t = TextRenderer.MeasureText(control.Text, control.Font).Width;
					i = Math.Max(t, i);
					continue;
				}

				if (control is TextBox)
				{
					var t = control.Width;
					j = Math.Max(t, j);
				}
			}

			return Convert.ToInt32(i + j + 43 * this.GetDeviceDpi());
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
