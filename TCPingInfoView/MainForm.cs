using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
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
			Icon = Resources.TCPing;
			notifyIcon1.Icon = Icon;
		}

		private delegate void VoidMethod_Delegate();

		public static bool QClose = false;

		public static int Timeout = 3000;
		public static int HighLatency = 300;
		public static Color TimeoutColor = Color.Red;
		public static Color HighLatencyColor = Color.Coral;
		public static Color LowLatencyColor = Color.Green;

		private int Needheight;
		private double listViewsProportion;
		private List<Data> _list;
		private ConcurrentList<TCPingLog> logs;

		private int _loadingFileTask = 0;
		private readonly object _lockloadingFileTask = new object();
		private int _loadinglogsTask = 0;
		private readonly object _lockloadinglogsTask = new object();
		private int _testAllTask = 0;
		private readonly object _locktestAllTask = new object();

		private readonly object _lockloadingMainList = new object();

		private delegate void VoidMethodDelegate();

		private Timer TestAllTimer;
		private const int second = 1000;
		private const int minute = 60 * second;
		public int interval = 1 * minute;

		private int xPos_MainlistView, yPos_MainlistView;

		#region MainlistView

		private int FindRealRow(int row)
		{
			for (var i = 0; i < _list.Count; ++i)
			{
				if (row + 1 == GetIndex1(i))
				{
					return i;
				}
			}
			throw new Exception(@"Logical Error");
		}

		private int? GetIndex1(int row)
		{
			int? res = null;
			MainlistView.Invoke(new VoidMethod_Delegate(() =>
			{
				res = Convert.ToInt32(MainlistView.Items[row].SubItems[0].Text);
			}));
			return res;
		}

		private void SetHostname1(int row, string hostname)
		{
			//lock (_lockloadingMainList)
			{
				MainlistView.Invoke(new VoidMethod_Delegate(() =>
				{
					row = FindRealRow(row);
					MainlistView.Items[row].SubItems[1].Text = hostname;
				}));
			}
		}

		private string GetHostname1(int row)
		{
			string res = null;
			MainlistView.Invoke(new VoidMethod_Delegate(() =>
			{
				res = MainlistView.Items[row].SubItems[1].Text;
			}));
			return res;
		}

		private void SetIPport1(int row, IPEndPoint ipEndPoint)
		{
			MainlistView.Invoke(new VoidMethod_Delegate(() =>
			{
				row = FindRealRow(row);
				MainlistView.Items[row].SubItems[2].Text = ipEndPoint.ToString();
			}));
		}

		private IPEndPoint GetIPport1(int row)
		{
			IPEndPoint res = null;
			MainlistView.Invoke(new VoidMethod_Delegate(() =>
			{
				row = FindRealRow(row);
				res = Util.ToIPEndPoint(MainlistView.Items[row].SubItems[2].Text, 443);
			}));
			return res;
		}

		private void SetLatency1(int row, int latency)
		{
			//lock (_lockloadingMainList)
			{
				MainlistView.Invoke(new VoidMethod_Delegate(() =>
				{
					row = FindRealRow(row);
					MainlistView.Items[row].SubItems[4].Text = latency.ToString();
					if (latency == Timeout)
					{
						SetLatencyColor1(row, TimeoutColor);
						MainlistView.Items[row].ImageIndex = 1;
					}
					else if (latency < HighLatency)
					{
						SetLatencyColor1(row, LowLatencyColor);
						MainlistView.Items[row].ImageIndex = 0;
					}
					else
					{
						SetLatencyColor1(row, HighLatencyColor);
						MainlistView.Items[row].ImageIndex = 0;
					}
				}));
			}
		}

		private double? GetLatency1(int row)
		{
			double? res = null;
			MainlistView.Invoke(new VoidMethod_Delegate(() =>
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
			//lock (_lockloadingMainList)
			{
				MainlistView.Invoke(new VoidMethod_Delegate(() =>
				{
					row = FindRealRow(row);
					MainlistView.Items[row].SubItems[5].Text = str;
				}));
			}
		}

		private string GetDescription1(int row)
		{
			string res = null;
			MainlistView.Invoke(new VoidMethod_Delegate(() =>
			{
				res = MainlistView.Items[row].SubItems[5].Text;
			}));
			return res;
		}

		private void SetFailedP1(int row, double failedP)
		{
			//lock (_lockloadingMainList)
			{
				MainlistView.Invoke(new VoidMethod_Delegate(() =>
				{
					row = FindRealRow(row);
					string str;
					if (Math.Abs(failedP) > 0.0)
					{
						str = failedP.ToString(@"P");
					}
					else
					{
						str = @"0%";
					}
					MainlistView.Items[row].SubItems[3].Text = str;
				}));
			}
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
					DatelistView.Items[row].ImageIndex = 1;
				}
				else if (latency < HighLatency)
				{
					SetLatencyColor2(row, LowLatencyColor);
					DatelistView.Items[row].ImageIndex = 0;
				}
				else
				{
					SetLatencyColor2(row, HighLatencyColor);
					DatelistView.Items[row].ImageIndex = 0;
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

		#region 窗口第一次载入

		private void MainForm_Load(object sender, EventArgs e)
		{
			Needheight = Height - (MainlistView.Height + DatelistView.Height);
			listViewsProportion = Convert.ToDouble(MainlistView.Height) / Convert.ToDouble(MainlistView.Height + DatelistView.Height);
			const string defaultPath = @"D:\Downloads\test.txt";
			if (File.Exists(defaultPath))
			{
				_list = Read.ReadAddressFromFile(defaultPath);
				LoadFromList();
			}
		}

		#endregion

		#region 选择文件载入

		private void LoadAddressFromFile()
		{
			var path = Read.GetFilePath();
			if (string.IsNullOrWhiteSpace(path))
			{
				return;
			}

			_list = Read.ReadAddressFromFile(path);
			LoadFromList();
		}

		private void 从文件载入ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadAddressFromFile();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			LoadAddressFromFile();
		}

		#endregion

		#region 载入主表格

		private async Task LoadFromLine(int index)
		{
			SetDescription1(index, _list[index].Description);
			if (Util.IsIPv4Address(_list[index].HostsName))//如果hostname是IP地址则反查DNS
			{
				SetIPport1(index, new IPEndPoint(_list[index].Ip, _list[index].Port));
				SetHostname1(index, _list[index].HostsName);
				_list[index].HostsName = await NetTest.GetHostNameAsync(IPAddress.Parse(_list[index].HostsName));
				SetHostname1(index, _list[index].HostsName);
			}
			else//如果hostname是域名则查询IP地址
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
				Monitor.Pulse(_lockloadingFileTask);
			}
		}

		/// <summary>
		/// 等待所有未处理的线程，清空所有列表，加载列表
		/// </summary>
		private void LoadFromList()
		{
			//lock (_lockloadingFileTask)
			//{
			//	while (_loadingFileTask != 0)
			//	{
			//		Monitor.Wait(_lockloadingFileTask);
			//	}
			//}

			lock (_locktestAllTask)
			{
				while (true)
				{
					if (_testAllTask == 0)
					{
						break;
					}
				}
			}

			lock (_lockloadinglogsTask)
			{
				while (true)
				{
					if (_loadinglogsTask == 0)
					{
						break;
					}
				}
			}


			MainlistView.Items.Clear();
			DatelistView.Items.Clear();
			var length = _list.Count;
			_loadingFileTask += length;
			for (var i = 0; i < length; ++i)
			{
				var emptyDatelistView = new ListViewItem
				{
					Text = $@"{i + 1}",
					SubItems =
					{
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
#pragma warning disable 4014
				Parallel.For(0, length, (i) => LoadFromLine(i));
#pragma warning restore 4014
			});
		}

		#endregion

		#region 测试所有

		private async void TestOne(int num)
		{
			if (_list[num].Ip == null)
			{
				await LoadFromLine(num);
				if (_list[num].Ip == null)
				{
					lock (_locktestAllTask)
					{
						--_testAllTask;
					}

					return;
				}
			}

			var ipe = new IPEndPoint(_list[num].Ip, _list[num].Port);
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
				latency = Convert.ToInt32(Math.Round(latency.Value));
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
			SetFailedP1(num, logs[num].FailedP);

			MainlistView.Invoke(() =>
			{
				if (MainlistView.SelectedItems.Count == 1)
				{
					var index1 = MainlistView.SelectedItems[0].Index;
					if (index1 == FindRealRow(num))
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

		private void TestAll()
		{
			var l = _list.Count;
			lock (_locktestAllTask)
			{
				_testAllTask += l;
				Monitor.Pulse(_locktestAllTask);
			}

			Task.Run(() => { Parallel.For(0, l, TestOne); });
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			TestAll();
		}

		#endregion

		#region 保持两表格的比例

		private void ChangeSize()
		{
			var height = Height - Needheight;
			MainlistView.Height = Convert.ToInt32(listViewsProportion * height);
			DatelistView.Height = height - MainlistView.Height;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			ChangeSize();
		}

		#endregion

		/// <summary>
		/// 关闭前是否确认
		/// </summary>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (!QClose)
				{
					e.Cancel = true;
					TriggerMainFormDisplay();
					return;
				}
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

		#region 主窗口显示隐藏

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void ShowHide_MenuItem_Click(object sender, EventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void TriggerMainFormDisplay()
		{
			Visible = !Visible;
			if (WindowState == FormWindowState.Minimized)
			{
				WindowState = FormWindowState.Normal;
			}
			TopMost = true;
			TopMost = false;
		}

		#endregion

		#region 循环Ping

		private void Start_Button_Click(object sender, EventArgs e)
		{
			TriggerRun();
		}

		private void StartStop_MenuItem_Click(object sender, EventArgs e)
		{
			TriggerRun();
		}

		private void StartCore(object state)
		{
			TestAll();
		}

		private void StartPing()
		{
			TestAllTimer?.Dispose();
			TestAllTimer = new Timer(StartCore, null, 0, interval);
			Start_Button.Text = @"停止";
			Start_Button.Image = Resources.Stop;
			StartStop_MenuItem.Text = @"停止";
		}

		private void StopPing()
		{
			TestAllTimer?.Dispose();
			Start_Button.Text = @"开始";
			Start_Button.Image = Resources.Start;
			StartStop_MenuItem.Text = @"开始";
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

		#endregion

		#region 退出程序

		private void Exit_MenuItem_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		#endregion

		#region 文件拖拽进主列表

		private void MainlistView_DragDrop(object sender, DragEventArgs e)
		{
			var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			_list = Read.ReadAddressFromFile(path);
			LoadFromList();
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

		#endregion

		#region 加载时间列表

		private void MainlistView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (MainlistView.SelectedIndices.Count <= 0)
			{
				return;
			}

			// ReSharper disable once PossibleInvalidOperationException
			LoadLogs(GetIndex1(MainlistView.SelectedIndices[0]).Value - 1);
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
			if (index < 0)
			{
				return;
			}

			var length = logs[index].Info.Count;
			if (length > 0)
			{
				_loadinglogsTask += length;

				for (var i = 0; i < length; ++i)
				{
					var emptyDatelistView = new ListViewItem { SubItems = { new ListViewItem.ListViewSubItem() } };
					DatelistView.Items.Add(emptyDatelistView);
				}

				Task.Run(() => { Parallel.For(0, length, i => { LoadLog(i, logs[index].Info[i]); }); });
			}
		}

		#endregion

		#region 主列表排序

		private void MainlistView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			lock (_lockloadingMainList)
			{
				Type type;
				if (e.Column == 0)
				{
					type = typeof(int);
				}
				else if (e.Column == 3)
				{
					type = typeof(double);
				}
				else if (e.Column == 4)
				{
					type = typeof(int);
				}
				else
				{
					type = typeof(string);
				}

				MainlistView.ListViewItemSorter = new ListViewItemComparer(e.Column, MainlistView.Sorting, type);

				if (MainlistView.Sorting == SortOrder.Ascending)
				{
					MainlistView.Sorting = SortOrder.Descending;
				}
				else
				{
					MainlistView.Sorting = SortOrder.Ascending;
				}

				MainlistView.Sort();
			}
		}

		#endregion

		#region 点击空白处清空时间列表

		private void MainlistView_MouseDown(object sender, MouseEventArgs e)
		{
			//Hit no item
			if (MainlistView.HitTest(xPos_MainlistView, yPos_MainlistView).Item == null)
			{
				LoadLogs(-1);
			}
		}

		private void MainlistView_MouseMove(object sender, MouseEventArgs e)
		{
			xPos_MainlistView = e.X;
			yPos_MainlistView = e.Y;
		}

		#endregion
	}
}
