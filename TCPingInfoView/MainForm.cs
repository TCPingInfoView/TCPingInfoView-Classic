using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPingInfoView
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public readonly int Timeout = 3000;
		private List<Data> _list;

		private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void LoadAddressFromFile()
		{
			var path = Read.GetFilePath();
			if (string.IsNullOrWhiteSpace(path))
			{
				return;
			}
			var sl = Read.ReadAddress(path);
			LoadFromList(sl);
		}

		private void 从文件载入ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadAddressFromFile();
		}

		private async void TestOne(int num)
		{
			var ipe = dataGridView1.Rows[num].Cells[2].Value as IPEndPoint;

			var latency = await NetTest.TCPing(ipe.Address, ipe.Port, Timeout);
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
				dataGridView1.Rows[num].Cells[3].Value = Timeout;
				dataGridView1.Rows[num].Cells[3].ToolTipText = @"超时";
				dataGridView1.Rows[num].Cells[3].Style.ForeColor = Color.Red;
			}
		}

		private void LoadFromLine(int index)
		{
			dataGridView1.Rows[index].Cells[0].Value = index + 1;
			dataGridView1.Rows[index].Cells[2].Value = _list[index].IpPort;
			dataGridView1.Rows[index].Cells[4].Value = _list[index].Description;
			var t = new Task(async () =>
			{
				if (Util.IsIPv4Address(_list[index].HostsName))
				{
					_list[index].HostsName = await NetTest.GetHostName(IPAddress.Parse(_list[index].HostsName));
				}
				dataGridView1.Rows[index].Cells[1].Value = _list[index].HostsName;
			});
			t.Start();
		}

		private async void LoadFromList(IEnumerable<string> sl)
		{
			dataGridView1.Rows.Clear();
			var l = await Util.ToData(sl);
			_list = l.ToList();
			var length = _list.Count;
			dataGridView1.Rows.Add(length);
			Parallel.For(0, length, LoadFromLine);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var sl = Read.ReadAddress(@"D:\Downloads\test.txt");
			LoadFromList(sl);
		}

		private void TestAll()
		{
			var l = dataGridView1.Rows.Count;
			Parallel.For(0, l, TestOne);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			TestAll();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			LoadAddressFromFile();
		}

		private void MainForm_SizeChanged(object sender, EventArgs e)
		{

		}
	}
}
