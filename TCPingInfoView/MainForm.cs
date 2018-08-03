using System;
using System.Net;
using System.Windows.Forms;

namespace TCPingInfoView
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public readonly int _timeout = 1000;

		private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void 从文件载入ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private async void Test2()
		{
			var ipe = dataGridView1.Rows[0].Cells[2].Value as IPEndPoint;

			var latency = await NetTest.TCPing(ipe.Address, ipe.Port, _timeout);
			if (latency != null)
			{
				dataGridView1.Rows[0].Cells[3].Value = Math.Round(latency.Value);
			}
			else
			{
				dataGridView1.Rows[0].Cells[3].Value = @"超时";
			}

		}

		private async void Test()
		{
			const string str = @"www.baidu.com:443 百度";
			var l = await Common.Stringline2Data(str);

			var index = dataGridView1.Rows.Add();
			dataGridView1.Rows[index].Cells[0].Value = index + 1;
			dataGridView1.Rows[index].Cells[1].Value = l.HostsName;
			dataGridView1.Rows[index].Cells[2].Value = l.IpPort;
			dataGridView1.Rows[index].Cells[4].Value = l.Description;
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			Test();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Test2();
		}
	}
}
