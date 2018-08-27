using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
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

		public int Timeout = 3000;
		public int HighLatency = 300;
		private int Needheight;
		private double dataGridViewsProportion;
		private List<Data> _list;
		private Thread _loadingFileTask = null;
		private Thread _testAllTask = null;

		#region dataGridViewCell

		private void SetdataGridView1Number(int row, int num)
		{
			dataGridView1.Rows[row].Cells[0].Value = num;
		}

		private int GetdataGridView1Number(int row)
		{
			return ((int?) dataGridView1.Rows[row].Cells[0].Value).Value;
		}
		#endregion

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

		private void TestOne(int num)
		{
			if (dataGridView1.Rows[num].Cells[2].Value == null)
			{
				if (dataGridView1.Rows[num].Cells[0].Value is int temp)
				{
					var index = temp - 1;
					_list[index].Ip = NetTest.GetIP(_list[index].HostsName);
					if (_list[index].Ip == null)
					{
						return;
					}
					dataGridView1.Rows[index].Cells[2].Value = new IPEndPoint(_list[index].Ip, _list[index].Port);
				}
			}
			var ipe = dataGridView1.Rows[num].Cells[2].Value as IPEndPoint;
			double? latency = null;

			try
			{
				latency = NetTest.TCPing(ipe.Address, ipe.Port, Timeout);
			}
			catch
			{
				//ignore
			}

			if (latency != null)
			{
				var value = Convert.ToInt32(Math.Round(latency.Value));
				dataGridView1.Rows[num].Cells[3].Value = value;
				if (value < HighLatency)
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
			dataGridView1.Rows[index].Cells[4].Value = _list[index].Description;
			if (Util.IsIPv4Address(_list[index].HostsName))
			{
				dataGridView1.Rows[index].Cells[2].Value = new IPEndPoint(_list[index].Ip, _list[index].Port);
				_list[index].HostsName = NetTest.GetHostName(IPAddress.Parse(_list[index].HostsName));
				dataGridView1.Rows[index].Cells[1].Value = _list[index].HostsName;
			}
			else
			{
				dataGridView1.Rows[index].Cells[1].Value = _list[index].HostsName;
				_list[index].Ip = NetTest.GetIP(_list[index].HostsName);
				if (_list[index].Ip == null)
				{
					dataGridView1.Rows[index].Cells[2].Value = null;
				}
				else
				{
					dataGridView1.Rows[index].Cells[2].Value = new IPEndPoint(_list[index].Ip, _list[index].Port);
				}
			}
		}

		private void LoadFromList(IEnumerable<string> sl)
		{
			while (_loadingFileTask != null)
			{

			}
			while (_testAllTask != null)
			{

			}

			dataGridView1.Rows.Clear();
			var l = Util.ToData(sl);
			_list = l.ToList();
			var length = _list.Count;
			dataGridView1.Rows.Add(length);

			Task.Run(() =>
			{
				_loadingFileTask = Thread.CurrentThread;
				Parallel.For(0, length, LoadFromLine);
				_loadingFileTask = null;
			});
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Needheight = Height - (dataGridView1.Height + dataGridView2.Height);
			dataGridViewsProportion = Convert.ToDouble(dataGridView1.Height) / Convert.ToDouble(dataGridView1.Height + dataGridView2.Height);
			const string defaultPath = @"D:\Downloads\test.txt";
			if (File.Exists(defaultPath))
			{
				var sl = Read.ReadAddress(defaultPath);
				LoadFromList(sl);
			}
		}

		private void TestAll()
		{
			var l = dataGridView1.Rows.Count;
			Task.Run(() =>
			{
				_testAllTask = Thread.CurrentThread;
				Parallel.For(0, l, TestOne);
				_testAllTask = null;
			});
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

		private void ChangeSize()
		{
			var height = Height - Needheight;
			dataGridView1.Height = Convert.ToInt32(dataGridViewsProportion * height);
			dataGridView2.Height = height - dataGridView1.Height;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			ChangeSize();
		}

		private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}

			if (e.Button == MouseButtons.Right)
			{
				dataGridView1.ClearSelection();
				dataGridView1.Rows[e.RowIndex].Selected = true;
			}
		}

		private void dataGridView1_DragDrop(object sender, DragEventArgs e)
		{
			var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			var sl = Read.ReadAddress(path);
			LoadFromList(sl);
		}

		private void dataGridView1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Link;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
	}
}
