using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

		public int Timeout = 3000;
		public int HighLatency = 300;
		private int Needheight;
		private double dataGridViewsProportion;
		private List<Data> _list;
		private int _loadingFileTask = 0;
		private readonly object _lockloadingFileTask = new object();
		private int _testAllTask = 0;
		private readonly object _locktestAllTask = new object();

		#region dataGridView1Cell

		private void SetIndex1(int row, int num)
		{
			dataGridView1.Rows[row].Cells[0].Value = num;
		}

		private int? GetIndex1(int row)
		{
			return dataGridView1.Rows[row].Cells[0].Value as int?;
		}

		private void SetHostname1(int row, string hostname)
		{
			dataGridView1.Rows[row].Cells[1].Value = hostname;
		}

		private string GetHostname1(int row)
		{
			return dataGridView1.Rows[row].Cells[1].Value as string;
		}

		private void SetIPport1(int row, IPEndPoint ipEndPoint)
		{
			dataGridView1.Rows[row].Cells[2].Value = ipEndPoint;
		}

		private IPEndPoint GetIPport1(int row)
		{
			return dataGridView1.Rows[row].Cells[2].Value as IPEndPoint;
		}

		private void SetLatency1(int row, double latency)
		{
			dataGridView1.Rows[row].Cells[3].Value = latency;
		}

		private double? GetLatency1(int row)
		{
			return dataGridView1.Rows[row].Cells[3].Value as double?;
		}

		private void SetDescription1(int row, string str)
		{
			dataGridView1.Rows[row].Cells[4].Value = str;
		}

		private string GetDescription1(int row)
		{
			return dataGridView1.Rows[row].Cells[4].Value as string;
		}

		private void SetLatencyColor1(int row, Color color)
		{
			dataGridView1.Rows[row].Cells[3].Style.ForeColor = color;
		}

		private void SetLatencyToolTip1(int row, string str)
		{
			dataGridView1.Rows[row].Cells[3].ToolTipText = str;
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

		private async void TestOne(int num)
		{
			if (GetIPport1(num) == null)
			{
				var temp = GetIndex1(num);
				if (temp != null)
				{
					var index = temp.Value - 1;
					_list[index].Ip = await NetTest.GetIPAsync(_list[index].HostsName);
					if (_list[index].Ip == null)
					{
						lock (_locktestAllTask)
						{
							--_testAllTask;
						}
						return;
					}
					SetIPport1(index, new IPEndPoint(_list[index].Ip, _list[index].Port));
				}
			}
			var ipe = GetIPport1(num);
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
				SetLatency1(num, value);
				if (value < HighLatency)
				{
					SetLatencyColor1(num, Color.Green);
				}
				else
				{
					SetLatencyColor1(num, Color.Coral);
				}
			}
			else
			{
				SetLatency1(num, Timeout);
				SetLatencyToolTip1(num, @"超时");
				SetLatencyColor1(num, Color.Red);
			}

			lock (_locktestAllTask)
			{
				--_testAllTask;
			}
		}

		private async void LoadFromLine(int index)
		{
			SetIndex1(index, index + 1);
			SetDescription1(index, _list[index].Description);
			if (Util.IsIPv4Address(_list[index].HostsName))
			{
				SetIPport1(index, new IPEndPoint(_list[index].Ip, _list[index].Port));
				_list[index].HostsName = await NetTest.GetHostNameAsync(IPAddress.Parse(_list[index].HostsName));
				SetHostname1(index, _list[index].HostsName);
			}
			else
			{
				SetHostname1(index, _list[index].HostsName);
				_list[index].Ip = await NetTest.GetIPAsync(_list[index].HostsName);
				if (_list[index].Ip == null)
				{
					SetIPport1(index, null);
				}
				else
				{
					SetIPport1(index, new IPEndPoint(_list[index].Ip, _list[index].Port));
				}
			}

			lock (_lockloadingFileTask)
			{
				--_loadingFileTask;
			}
		}

		private void LoadFromList(IEnumerable<string> sl)
		{
			while (true)
			{
				if (_loadingFileTask == 0)
				{
					break;
				}
			}

			while (true)
			{
				if (_testAllTask == 0)
				{
					break;
				}
			}

			dataGridView1.Rows.Clear();
			var l = Util.ToData(sl);
			_list = l.ToList();
			var length = _list.Count;
			_loadingFileTask = length;
			dataGridView1.Rows.Add(length);
			Task.Run(() =>
			{
				Parallel.For(0, length, LoadFromLine);
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
			lock (_locktestAllTask)
			{
				_testAllTask += l;
			}
			Task.Run(() =>
			{
				Parallel.For(0, l, TestOne);
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
