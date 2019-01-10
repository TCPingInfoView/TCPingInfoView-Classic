using System;
using System.Windows.Forms;
using TCPingInfoView.I18n;
using TCPingInfoView.Model;

namespace TCPingInfoView.Forms
{
	public partial class TCPingOptionsForm : Form
	{
		public TCPingOptionsForm(TCPingOptions options, string list)
		{
			InitializeComponent();
			Setting = options;
			List = list;
		}

		public readonly TCPingOptions Setting;
		public string List;

		private void LoadI18N()
		{
			Text = I18N.GetString(@"TCPing Options");
			OK_button.Text = I18N.GetString(@"OK");
			Cancel_button.Text = I18N.GetString(@"Cancel");
			label1.Text = I18N.GetString(@"Endpoints List:");
			label2.Text = I18N.GetString(@"Reverse DNS Timeout(ms):");
			label3.Text = I18N.GetString(@"TCPing Timeout(ms):");
			label4.Text = I18N.GetString(@"High Latency(ms):");
			label5.Text = I18N.GetString(@"TCPing Interval(seconds):");
			groupBox2.Text = I18N.GetString(@"Windows Title");
			groupBox3.Text = I18N.GetString(@"Color of High Latency");
			groupBox4.Text = I18N.GetString(@"Color of Timeout");
			groupBox5.Text = I18N.GetString(@"Color of Low Latency");
		}

		private void LoadSetting()
		{
			textBox1.Text = List;
			textBox2.Text = Setting.Title;

			DNSTimeoutPing.Value = Setting.ReverseDnsTimeout;
			TimeoutPing.Value = Setting.Timeout;
			HighPing.Value = Setting.HighLatency;
			PingInterval.Value = Setting.TCPingInterval;

			Timeout_button.BackColor = Setting.TimeoutColor;
			HighPing_button.BackColor = Setting.HighLatencyColor;
			LowPing_button.BackColor = Setting.LowLatencyColor;
		}

		private void SaveSetting()
		{
			List = textBox1.Text;
			Setting.Title = textBox2.Text;

			Setting.ReverseDnsTimeout = Convert.ToInt32(DNSTimeoutPing.Value);
			Setting.Timeout = Convert.ToInt32(TimeoutPing.Value);
			Setting.HighLatency = Convert.ToInt32(HighPing.Value);
			Setting.TCPingInterval = Convert.ToInt32(PingInterval.Value);

			Setting.TimeoutColor = Timeout_button.BackColor;
			Setting.HighLatencyColor = HighPing_button.BackColor;
			Setting.LowLatencyColor = LowPing_button.BackColor;
		}

		private void TCPingOptionsForm_Load(object sender, EventArgs e)
		{
			LoadI18N();
			LoadSetting();
		}

		private void ColorButton_Click(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var colorDia = new ColorDialog();
			if (colorDia.ShowDialog() == DialogResult.OK)
			{
				var color = colorDia.Color;
				button.BackColor = color;
			}
		}

		private void HighPing_ValueChanged(object sender, EventArgs e)
		{
			HighPing.Maximum = TimeoutPing.Value;
		}

		private void OK_button_Click(object sender, EventArgs e)
		{
			SaveSetting();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void Cancel_button_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
