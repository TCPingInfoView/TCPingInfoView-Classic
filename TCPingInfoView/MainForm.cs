using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPingInfoView.Properties;
using Timer = System.Threading.Timer;

namespace TCPingInfoView
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			Icon = Resources.huaji128;
			notifyIcon1.Icon = Resources.huaji128;
		}

		private delegate void VoidMethod_Delegate();

		public static bool QClose = false;

		public static int Timeout = 3000;
		public static int HighLatency = 300;
		public static Color TimeoutColor = Color.Red;
		public static Color HighLatencyColor = Color.Coral;
		public static Color LowLatencyColor = Color.Green;

		private int Needheight;
		private double dataGridViewsProportion;
		private List<Data> _list;
		private ConcurrentList<TCPingLog> logs;

		private int _loadingFileTask = 0;
		private readonly object _lockloadingFileTask = new object();
		private int _loadinglogsTask = 0;
		private readonly object _lockloadinglogsTask = new object();
		private int _testAllTask = 0;
		private readonly object _locktestAllTask = new object();

		#region MainlistView

		private void SetIndex1(int row, int num)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				MainlistView.Items[row].SubItems[0].Text = num.ToString();
			}));
		}

		private int? GetIndex1(int row)
		{
			int? res = null;
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = Convert.ToInt32(MainlistView.Items[row].SubItems[0].Text);
			}));
			return res;
		}

		private void SetHostname1(int row, string hostname)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				MainlistView.Items[row].SubItems[1].Text = hostname;
			}));
		}

		private string GetHostname1(int row)
		{
			string res = null;
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = MainlistView.Items[row].SubItems[1].Text;
			}));
			return res;
		}

		private void SetIPport1(int row, IPEndPoint ipEndPoint)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				MainlistView.Items[row].SubItems[2].Text = ipEndPoint.ToString();
			}));
		}

		private IPEndPoint GetIPport1(int row)
		{
			IPEndPoint res = null;
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = Util.ToIPEndPoint(MainlistView.Items[row].SubItems[2].Text, 443);
			}));
			return res;
		}

		private void SetLatency1(int row, int latency)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				MainlistView.Items[row].SubItems[4].Text = latency.ToString();
				if (latency == Timeout)
				{
					SetLatencyColor1(row, TimeoutColor);
				}
				else if (latency < HighLatency)
				{
					SetLatencyColor1(row, LowLatencyColor);
				}
				else
				{
					SetLatencyColor1(row, HighLatencyColor);
				}
			}));
		}

		private double? GetLatency1(int row)
		{
			double? res = null;
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = Convert.ToDouble(MainlistView.Items[row].SubItems[4].Text);
			}));
			return res;
		}

		private void SetLatencyColor1(int row, Color color)
		{
			MainlistView.Items[row].UseItemStyleForSubItems = false;
			MainlistView.Items[row].SubItems[4].ForeColor = color;
		}

		private void SetDescription1(int row, string str)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				MainlistView.Items[row].SubItems[5].Text = str;
			}));
		}

		private string GetDescription1(int row)
		{
			string res = null;
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = MainlistView.Items[row].SubItems[5].Text;
			}));
			return res;
		}

		private void SetFailedP1(int row, string str)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				MainlistView.Items[row].SubItems[3].Text = str;
			}));
		}

		#endregion

		#region DatelistView

		private void SetDate2(int row, DateTime num)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				DatelistView.Items[row].SubItems[0].Text = num.ToString(CultureInfo.CurrentCulture);
			}));
		}

		private DateTime GetDate2(int row)
		{
			var res = new DateTime();
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = DateTime.Parse(DatelistView.Items[row].SubItems[0].Text);
			}));
			return res;
		}

		private void SetLatency2(int row, int latency)
		{
			BeginInvoke(new VoidMethod_Delegate(() =>
			{
				DatelistView.Items[row].SubItems[1].Text = latency.ToString();
				if (latency == Timeout)
				{
					SetLatencyColor2(row, TimeoutColor);
				}
				else if (latency < HighLatency)
				{
					SetLatencyColor2(row, LowLatencyColor);
				}
				else
				{
					SetLatencyColor2(row, HighLatencyColor);
				}
			}));
		}

		private double? GetLatency2(int row)
		{
			double? res = null;
			Invoke(new VoidMethod_Delegate(() =>
			{
				res = Convert.ToDouble(DatelistView.Items[row].SubItems[1].Text);
			}));
			return res;
		}

		private void SetLatencyColor2(int row, Color color)
		{
			DatelistView.Items[row].UseItemStyleForSubItems = false;
			DatelistView.Items[row].SubItems[1].ForeColor = color;
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
			var date = DateTime.Now;
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
			}
			else
			{
				latency = Timeout;
			}

			var log = new TCPingInfo
			{
				Date = date,
				Latenty = latency.Value
			};
			logs[num].Add(log);

			SetLatency1(num, (int)latency.Value);
			var failedP = logs[num].FailedP;
			if (Math.Abs(failedP) > 0.0)
			{
				SetFailedP1(num, failedP.ToString(@"P"));
			}
			else
			{
				SetFailedP1(num, @"0%");
			}

			MainlistView.Invoke(() =>
			{
				if (MainlistView.SelectedItems.Count == 1)
				{
					var index1 = MainlistView.SelectedItems[0].Index;
					if (index1 == num)
					{
						var emptyDatelistView = new ListViewItem { SubItems = { new ListViewItem.ListViewSubItem() } };
						var index2 = DatelistView.Items.Add(emptyDatelistView).Index;
						lock (_lockloadinglogsTask)
						{
							++_loadinglogsTask;
						}

						LoadLog(index2, log);
					}
				}
			});


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

			while (true)
			{
				if (_loadinglogsTask == 0)
				{
					break;
				}
			}

			MainlistView.Items.Clear();
			DatelistView.Items.Clear();
			var l = Util.ToData(sl);
			_list = l.ToList();
			var length = _list.Count;
			_loadingFileTask += length;
			for (var i = 0; i < length; ++i)
			{
				var emptyDatelistView = new ListViewItem
				{
					SubItems =
					{
							new ListViewItem.ListViewSubItem(),
							new ListViewItem.ListViewSubItem(),
							new ListViewItem.ListViewSubItem(),
							new ListViewItem.ListViewSubItem(),
							new ListViewItem.ListViewSubItem(),
							new ListViewItem.ListViewSubItem(),
					}
				};
				MainlistView.Items.Add(emptyDatelistView);
			}
			logs = new ConcurrentList<TCPingLog>();
			for (var i = 0; i < length; ++i)
			{
				var emptylog = new TCPingLog();
				logs.Add(emptylog);
			}
			Task.Run(() =>
			{
				Parallel.For(0, length, LoadFromLine);
			});
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Needheight = Height - (MainlistView.Height + DatelistView.Height);
			dataGridViewsProportion = Convert.ToDouble(MainlistView.Height) / Convert.ToDouble(MainlistView.Height + DatelistView.Height);
			const string defaultPath = @"D:\Downloads\test.txt";
			if (File.Exists(defaultPath))
			{
				var sl = Read.ReadAddress(defaultPath);
				LoadFromList(sl);
			}
		}

		private void TestAll()
		{
			var l = _list.Count;
			lock (_locktestAllTask)
			{
				_testAllTask += l;
			}
			Task.Run(() =>
			{
				Parallel.For(0, l, TestOne);
			});
		}

		private void LoadLog(int i, TCPingInfo log)
		{
			SetDate2(i, log.Date);
			var latency = Convert.ToInt32(Math.Round(log.Latenty));
			SetLatency2(i, latency);

			lock (_lockloadinglogsTask)
			{
				--_loadinglogsTask;
			}
		}

		private void LoadLogs(int index)
		{
			while (true)
			{
				if (_loadinglogsTask == 0)
				{
					break;
				}
			}
			DatelistView.Items.Clear();
			var length = logs[index].Info.Count;
			if (length > 0)
			{
				_loadinglogsTask += length;

				for (var i = 0; i < length; ++i)
				{
					var emptyDatelistView = new ListViewItem { SubItems = { new ListViewItem.ListViewSubItem() } };
					DatelistView.Items.Add(emptyDatelistView);
				}
				Task.Run(() =>
				{
					Parallel.For(0, length, i =>
					{
						LoadLog(i, logs[index].Info[i]);
					});
				});
			}
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
			MainlistView.Height = Convert.ToInt32(dataGridViewsProportion * height);
			DatelistView.Height = height - MainlistView.Height;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			ChangeSize();
		}

		private void TriggerRun()
		{
			Start_Button.Enabled = false;
			StartStop_MenuItem.Enabled = false;
			VoidMethodDelegate method;
			if (Start_Button.Text == @"开始")
			{
				method = StartPing;
			}
			else
			{
				method = StopPing;
			}

			var t = new Task(() => { method(); });
			t.Start();
			t.ContinueWith(task =>
			{
				BeginInvoke(new VoidMethodDelegate(() =>
				{
					Start_Button.Enabled = true;
					StartStop_MenuItem.Enabled = true;
				}));
			});
		}

		private void Start_Button_Click(object sender, EventArgs e)
		{
			TriggerRun();
		}

		private delegate void VoidMethodDelegate();
		private Timer TestAllTimer;
		private const int second = 1000;
		private const int minute = 60 * second;
		public int interval = 1 * minute;

		private void StartCore(object state)
		{
			TestAll();
		}

		private void StartPing()
		{
			TestAllTimer?.Dispose();
			TestAllTimer = new Timer(StartCore, null, 0, interval);
			Start_Button.Text = @"停止";
			StartStop_MenuItem.Text = @"停止";
		}

		private void StopPing()
		{
			TestAllTimer?.Dispose();
			Start_Button.Text = @"开始";
			StartStop_MenuItem.Text = @"开始";
		}

		private void TriggerMainFormDisplay()
		{
			Visible = !Visible;
			if (WindowState == FormWindowState.Minimized)
				WindowState = FormWindowState.Normal;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!QClose)
			{
				e.Cancel = true;
				TriggerMainFormDisplay();
				return;
			}
			if (e.CloseReason == CloseReason.UserClosing)
			{
				var dr = MessageBox.Show(@"「是」退出，「否」最小化", @"是否退出？", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					Dispose();
					Application.Exit();
				}
				else if (dr == DialogResult.No)
				{
					e.Cancel = true;
					TriggerMainFormDisplay();
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void ShowHide_MenuItem_Click(object sender, EventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void StartStop_MenuItem_Click(object sender, EventArgs e)
		{
			TriggerRun();
		}

		private void Exit_MenuItem_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void MainlistView_DragDrop(object sender, DragEventArgs e)
		{
			var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			var sl = Read.ReadAddress(path);
			LoadFromList(sl);
		}

		private void MainlistView_DragEnter(object sender, DragEventArgs e)
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

		private void MainlistView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (MainlistView.SelectedIndices.Count <= 0)
			{
				return;
			}

			LoadLogs(MainlistView.SelectedIndices[0]);
		}
	}
}
