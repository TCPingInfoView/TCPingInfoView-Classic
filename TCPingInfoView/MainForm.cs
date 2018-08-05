using System;
using System.Collections.Generic;
using System.Drawing;
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
			var path = Read.GetFilePath();
			if (string.IsNullOrWhiteSpace(path))
			{
				return;
			}
			var sl = Read.ReadAddress(path);
			dataGridView1.Rows.Clear();
			LoadFromList(sl);
		}

		private async void TestOne(int num)
		{
			var ipe = dataGridView1.Rows[num].Cells[2].Value as IPEndPoint;

			var latency = await NetTest.TCPing(ipe.Address, ipe.Port, _timeout);
			if (latency != null)
			{
				var value = Convert.ToInt32(Math.Round(latency.Value));
				dataGridView1.Rows[num].Cells[3].Value = value;
				if (value <= 300)
				{
					dataGridView1.Rows[num].Cells[3].Style.ForeColor = Color.Green;
				}
				else
				{
					dataGridView1.Rows[num].Cells[3].Style.ForeColor = Color.Coral;
				}
			}
			else
			{
				dataGridView1.Rows[num].Cells[3].Value = _timeout;
				dataGridView1.Rows[num].Cells[3].ToolTipText = @"超时";
				dataGridView1.Rows[num].Cells[3].Style.ForeColor = Color.Red;
			}
		}

		private async void LoadFromLine(string line)
		{
			var l = await Common.Stringline2Data(line);

			var index = dataGridView1.Rows.Add();
			dataGridView1.Rows[index].Cells[0].Value = index + 1;
			dataGridView1.Rows[index].Cells[1].Value = l.HostsName;
			dataGridView1.Rows[index].Cells[2].Value = l.IpPort;
			dataGridView1.Rows[index].Cells[4].Value = l.Description;
		}

		private void LoadFromList(IEnumerable<string> sl)
		{
			foreach (var s in sl)
			{
				LoadFromLine(s);
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var sl = Read.ReadAddress(@"D:\Downloads\test.txt");
			LoadFromList(sl);
		}

		private void TestAll()
		{
			var l = dataGridView1.Rows.Count;
			for (var i = 0; i < l; ++i)
			{
				TestOne(i);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			TestAll();
		}
	}
}
