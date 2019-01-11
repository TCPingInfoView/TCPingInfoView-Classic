using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPingInfoView.Collection;
using TCPingInfoView.Control;
using TCPingInfoView.I18n;
using TCPingInfoView.Model;
using TCPingInfoView.NetUtils;
using TCPingInfoView.Properties;
using TCPingInfoView.Steamworks;
using TCPingInfoView.Util;
using Timer = System.Threading.Timer;

namespace TCPingInfoView.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			//之后再绑定 ContextMenuStrip，否则可能出BUG
			//Another Fix:https://github.com/HMBSbige/TCPingInfoView/commit/e058c15dc7265358ed08f75765d62f9ece6b410e#diff-654cfc907abeeabf4121da2c3df241e7L143
			File_MenuItem.DropDown = NotifyIcon_MenuStrip;
			View_MenuItem.DropDown = MainList_MenuStrip;
			//Load Icon
			Icon = Resources.TCPing;
			notifyIcon1.Icon = Resources.TCPing;
			Config.Load();
		}

		private static string ExeName => Assembly.GetExecutingAssembly().GetName().Name;

		private readonly AppConfig Config =
				new AppConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{ExeName}.json"));

		private static string ListPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{ExeName}.txt");

		#region DPI参数

		private double Dpi => this.GetDeviceDpi();
		private static Size DefPicSize => new Size(16, 16);

		private Size DpiPicSize =>
				new Size(Convert.ToInt32(DefPicSize.Width * Dpi), Convert.ToInt32(DefPicSize.Height * Dpi));

		#endregion

		#region 表格显示参数

		private double ListRatio;
		public const int ColumnsCount = 12;

		#endregion

		#region 杂项设置

		private bool _isNotifyClose;
		private bool _isShowDateList;
		private bool _isLoadSetting;
		private FormWindowState DefaultState = FormWindowState.Normal;
		private int _mainListMouseLocationX, _mainListMouseLocationY;
		private string UserTitle;

		#endregion

		#region 超时设置

		public static int ReverseDNSTimeout = 3000;
		public static int Timeout = 3000;
		public static int HighLatency = 300;
		public static Color TimeoutColor = Color.Red;
		public static Color HighLatencyColor = Color.Coral;
		public static Color LowLatencyColor = Color.Green;

		#endregion

		#region 列表相关数据结构

		private string RawString;

		private ConcurrentList<Data> rawTable = new ConcurrentList<Data>();

		private readonly BindingCollection<MainTable> MainTable = new BindingCollection<MainTable>();
		private ConcurrentList<MainTable> mainTable = new ConcurrentList<MainTable>();

		private readonly BindingCollection<DateTable> DateTable = new BindingCollection<DateTable>();
		private ConcurrentList<DateTable> dateTable = new ConcurrentList<DateTable>();

		#endregion

		#region PingTask

		private ConcurrentList<Task> PingTasks = new ConcurrentList<Task>();
		private static CancellationTokenSource cts_PingTask = new CancellationTokenSource();

		#endregion

		#region Timer

		private Timer TestAllTimer;
		private const int second = 1000;
		private const int minute = 60 * second;
		private int interval = 1 * minute;

		#endregion

		#region DPI改变

		private void MainForm_DpiChanged(object sender, DpiChangedEventArgs e)
		{
			Debug.WriteLine($@"DPI:{e.DeviceDpiOld}=>{e.DeviceDpiNew}	{this.GetDeviceDpi() * 100}%");
			LoadControlsByDpi();
			Task.Run(() =>
			{
				this.Invoke(() =>
				{
					++Height;
					--Height;
				});
			});
		}

		private void LoadControlsByDpi()
		{
			if (Dpi > 1.0)
			{
				Test_Button.ImageScaling = ToolStripItemImageScaling.None;
				Test_Button.Image = Util.Util.ResizeImage(Resources.Test, DpiPicSize);
				Start_Button.ImageScaling = ToolStripItemImageScaling.None;
				Start_Button.Image = Util.Util.ResizeImage(Resources.Start, DpiPicSize);
				Exit_Button.ImageScaling = ToolStripItemImageScaling.None;
				Exit_Button.Image = Util.Util.ResizeImage(Resources.Exit, DpiPicSize);
				Load_Button.ImageScaling = ToolStripItemImageScaling.None;
				Load_Button.Image = Util.Util.ResizeImage(Resources.Load, DpiPicSize);
				Minimize_Button.ImageScaling = ToolStripItemImageScaling.None;
				Minimize_Button.Image = Util.Util.ResizeImage(Resources.Minimize, DpiPicSize);
				List_Button.ImageScaling = ToolStripItemImageScaling.None;
				List_Button.Image = Util.Util.ResizeImage(Resources.List, DpiPicSize);
			}
			else
			{
				Test_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Test_Button.Image = Resources.Test;
				Start_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Exit_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Load_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Minimize_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				List_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			}
		}

		#endregion

		#region 窗口第一次载入

		private void LoadLanguage()
		{
			List_Button.Text = I18N.GetString(@"TCPing Options");
			Load_Button.Text = I18N.GetString(@"Load");
			Test_Button.Text = I18N.GetString(@"Test");
			Start_Button.Text = I18N.GetString(@"Start");
			Search_TextBox.CueBanner = I18N.GetString(@"Find");
			Minimize_Button.Text = I18N.GetString(@"Minimize to Tray");
			Exit_Button.Text = I18N.GetString(@"Exit");

			File_MenuItem.Text = I18N.GetString(@"&File");
			View_MenuItem.Text = I18N.GetString(@"&View");
			Options_MenuItem.Text = I18N.GetString(@"&Options");
			Help_MenuItem.Text = I18N.GetString(@"&Help");

			ShowHide_MenuItem.Text = I18N.GetString(@"Show/Hide");
			LoadFile_MenuItem.Text = I18N.GetString(@"Load Endpoints From File");
			StartStop_MenuItem.Text = I18N.GetString(@"Start");
			Reset_MenuItem.Text = I18N.GetString(@"Reset");
			Exit_MenuItem.Text = I18N.GetString(@"Exit");

			AutoColumnsSize_MenuItem.Text = I18N.GetString(@"Auto Size Columns Except Headers");
			AutoColumnsSizeAndHeader_MenuItem.Text = I18N.GetString(@"Auto Size Columns");
			DisplayedColumns_MenuItem.Text = I18N.GetString(@"Choose Columns");
			ShowLogForm_MenuItem.Text = I18N.GetString(@"Properties");

			TCPingOptions_MenuItem.Text = I18N.GetString(@"TCPing Options");
			IsNotifyClose_MenuItem.Text = I18N.GetString(@"Confirm Before Closing the Window");
			IsShowDateList_MenuItem.Text = I18N.GetString(@"Show Log List");

			About_MenuItem.Text = I18N.GetString(@"About");
		}

		private void LoadMainList()
		{
			MainList.Columns[0].HeaderText = I18N.GetString(@"Order");
			MainList.Columns[1].HeaderText = I18N.GetString(@"Host Name");
			MainList.Columns[2].HeaderText = I18N.GetString(@"IP:Port");
			MainList.Columns[3].HeaderText = I18N.GetString(@"% Failed");
			MainList.Columns[4].HeaderText = I18N.GetString(@"Latency(ms)");
			MainList.Columns[5].HeaderText = I18N.GetString(@"Description");
			MainList.Columns[6].HeaderText = I18N.GetString(@"Average TCPing Latency(ms)");
			MainList.Columns[7].HeaderText = I18N.GetString(@"% Succeed");
			MainList.Columns[8].HeaderText = I18N.GetString(@"Succeed Count");
			MainList.Columns[9].HeaderText = I18N.GetString(@"Failed Count");
			MainList.Columns[10].HeaderText = I18N.GetString(@"Max TCPing Latency(ms)");
			MainList.Columns[11].HeaderText = I18N.GetString(@"Min TCPing Latency(ms)");

			MainList.Columns[0].DataPropertyName = @"Index";
			MainList.Columns[1].DataPropertyName = @"HostsName";
			MainList.Columns[2].DataPropertyName = @"Endpoint";
			MainList.Columns[3].DataPropertyName = @"FailedP";
			MainList.Columns[4].DataPropertyName = @"LastPing";
			MainList.Columns[5].DataPropertyName = @"Description";
			MainList.Columns[6].DataPropertyName = @"Average";
			MainList.Columns[7].DataPropertyName = @"SucceedP";
			MainList.Columns[8].DataPropertyName = @"SucceedCount";
			MainList.Columns[9].DataPropertyName = @"FailedCount";
			MainList.Columns[10].DataPropertyName = @"MaxPing";
			MainList.Columns[11].DataPropertyName = @"MinPing";
		}

		private void LoadDateList()
		{
			DateList.Columns[0].HeaderText = I18N.GetString(@"Sent On");
			DateList.Columns[1].HeaderText = I18N.GetString(@"Latency(ms)");

			DateList.Columns[0].DataPropertyName = @"Date";
			DateList.Columns[1].DataPropertyName = @"Latency";
		}

		private void LoadSetting()
		{
			Height = Config.MainFormHeight;
			Width = Config.MainFormWidth;
			DateList.Height = Config.DateListHeight;

			if (Util.Util.IsOnScreen(new Point(Config.StartPositionLeft, Config.StartPositionTop), this))
			{
				StartPosition = FormStartPosition.Manual;
				Location = new Point(Config.StartPositionLeft, Config.StartPositionTop);
			}
			else
			{
				StartPosition = FormStartPosition.CenterScreen;
			}

			IsNotifyClose_MenuItem.Checked = Config.IsNotifyClose;
			IsShowDateList_MenuItem.CheckState = Config.IsShowDateList ? CheckState.Checked : CheckState.Unchecked;

			for (var i = 0; i < ColumnsCount; ++i)
			{
				MainList.Columns[i].DisplayIndex = Config.ColumnsOrder[i];
				MainList.Columns[i].Visible = Config.ColumnsWidth[i] != 0;
			}

			for (var i = 0; i < ColumnsCount - 1; ++i)
			{
				for (var j = 0; j < ColumnsCount; ++j)
				{
					if (MainList.Columns[j].DisplayIndex == i)
					{
						if (Config.ColumnsWidth[j] != 0)
						{
							MainList.Columns[j].Width = Config.ColumnsWidth[j];
						}
					}
				}
			}

			LoadTCPingOptions(Config.TCPingOptions);

			LoadTitle();

			_isLoadSetting = true;
		}

		private void LoadTCPingOptions(TCPingOptions setting)
		{
			UserTitle = setting.Title;

			ReverseDNSTimeout = setting.ReverseDnsTimeout;
			Timeout = setting.Timeout;
			HighLatency = setting.HighLatency;
			interval = setting.TCPingInterval * second; //s->ms

			TimeoutColor = setting.TimeoutColor;
			HighLatencyColor = setting.HighLatencyColor;
			LowLatencyColor = setting.LowLatencyColor;
		}

		private void SetMiniSize()
		{
			var miniHeight = Height - ClientRectangle.Height + menuStrip1.Height + toolStrip1.Height + statusStrip1.Height;

			var h1 = MainList.RowTemplate.Height + MainList.ColumnHeadersHeight;
			miniHeight += h1;
			splitter1.MinExtra = h1;
			MainList.MinimumSize = new Size(0, h1);

			var h2 = DateList.RowTemplate.Height + DateList.ColumnHeadersHeight;
			miniHeight += h2;
			splitter1.MinSize = h2;
			DateList.MinimumSize = new Size(0, h2);

			miniHeight += splitter1.Height;
			MinimumSize = new Size(0, miniHeight);
		}

		private void LoadTitle()
		{
			if (SteamManager.IsLoaded)
			{
				Text = $@"{ExeName} - {SteamManager.SteamWorksClient.Username}";
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			LoadLanguage();

			if (!DpiUtils.CheckHighDpiEnvironment())
			{
				MessageBox.Show(I18N.GetString(@"TCPingInfoView may not be able to adapt to your high DPI environment properly!"), I18N.GetString(@"High DPI Environment Check"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			LoadSetting();

			SetMiniSize();

			ChangedRatio();

			LoadControlsByDpi();

			MainList.AutoGenerateColumns = false;
			MainList.DataSource = MainTable;
			LoadMainList();

			DateList.AutoGenerateColumns = false;
			DateList.DataSource = DateTable;
			LoadDateList();

			if (File.Exists(ListPath))
			{
				RawString = Read.ReadTextFromFile(ListPath);
				rawTable = Read.ReadAddressFromString(RawString);
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

			RawString = Read.ReadTextFromFile(path);
			rawTable = Read.ReadAddressFromString(RawString);
			LoadFromList();
		}

		private void LoadFile_MenuItem_Click(object sender, EventArgs e)
		{
			LoadAddressFromFile();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			LoadAddressFromFile();
		}

		#endregion

		#region 载入主表格

		private void ToMainTable(IEnumerable<MainTable> table)
		{
			foreach (var item in table)
			{
				if (MainTable.All(x => x.Index != item.Index))
				{
					MainTable.Add(item);
				}
			}
		}

		/// <summary>
		/// 等待所有未处理的线程，清空所有列表，加载列表
		/// </summary>
		private void LoadFromList()
		{
			StopPing();
			cts_PingTask.Cancel();
			Util.Util.RemoveCompletedTasks(ref PingTasks);
			Task.WaitAll(PingTasks.ToArray());
			cts_PingTask.Dispose();
			cts_PingTask = new CancellationTokenSource();
			PingTasks = new ConcurrentList<Task>();

			MainTable.Clear();
			DateTable.Clear();

			mainTable = Util.Util.ToMainTable(rawTable);
			ToMainTable(mainTable);
			if (rawTable.Count > 0)
			{
				MainList.Rows[0].Selected = true;
			}

			FirstPing();
		}

		#endregion

		#region TCPing 核心

		private void FirstPing()
		{
			var t = new Task(() =>
			{
				Parallel.For(0, mainTable.Count, (i, state) =>
				{
					if (cts_PingTask.IsCancellationRequested)
					{
						state.Stop();
						return;
					}

					if (IPFormatter.IsIPv4Address(mainTable[i].HostsName)) //反查DNS
					{
						PingOne(i);

						if (cts_PingTask.IsCancellationRequested)
						{
							state.Stop();
							return;
						}

						mainTable[i].HostsName = NetTest.GetHostName(IPAddress.Parse(mainTable[i].HostsName), ReverseDNSTimeout);
					}
					else
					{
						var ip = NetTest.GetIP(mainTable[i].HostsName);

						if (cts_PingTask.IsCancellationRequested)
						{
							state.Stop();
							return;
						}

						if (ip != null)
						{
							mainTable[i].Endpoint = $@"{ip}:{rawTable[i].Port}";
						}
						PingOne(i);
					}

					if (cts_PingTask.IsCancellationRequested)
					{
						state.Stop();
					}
				});
				//notifyIcon1.ShowBalloonTip(1000, ExeName, @"载入完毕", ToolTipIcon.Info);
			});
			PingTasks.Add(t);
			t.Start();
		}

		private void PingOne(int index)
		{
			if (mainTable[index].Endpoint == string.Empty)
			{
				var ip = NetTest.GetIP(mainTable[index].HostsName);

				if (ip != null)
				{
					mainTable[index].Endpoint = $@"{ip}:{rawTable[index].Port}";
				}
				else
				{
					return;
				}
			}

			var ipe = IPFormatter.ToIPEndPoint(mainTable[index].Endpoint, 443);
			double? latency = null;
			var res = Timeout;
			var time = DateTime.Now;
			try
			{
				latency = NetTest.TCPing(ipe.Address, ipe.Port, Timeout);
			}
			catch
			{
				// ignored
			}

			if (latency != null)
			{
				res = Convert.ToInt32(Math.Round(latency.Value));
			}
			else
			{
				//notifyIcon1.ShowBalloonTip(1000, time.ToString(CultureInfo.CurrentCulture), $"{mainTable[index].HostsName}\n{ipe}", ToolTipIcon.Error);
			}

			var log = new DateTable
			{
				Date = time,
				Latency = res
			};

			mainTable[index].AddNewLog(log);

			if (MainList.SelectedRows.Count == 1)
			{
				var i = MainList.SelectedRows[0].Cells[0].Value as int?;
				if (i == index && DateList.Visible)
				{
					DateList.Invoke(() => { LoadLogs(log); });
				}
			}
		}

		private void PingAll()
		{
			var t = new Task(() =>
			{
				Parallel.For(0, mainTable.Count, (i, state) =>
				{
					if (cts_PingTask.IsCancellationRequested)
					{
						state.Stop();
						return;
					}

					PingOne(i);

					if (cts_PingTask.IsCancellationRequested)
					{
						state.Stop();
					}
				});
			});
			Util.Util.RemoveCompletedTasks(ref PingTasks);
			PingTasks.Add(t);
			t.Start();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			PingAll();
		}

		#endregion

		#region 窗口改变保持表格比例

		private void ChangedSize()
		{
			var height = Height - MinimumSize.Height;
			DateList.Height = Convert.ToInt32(ListRatio * height);
		}

		private void ChangedRatio()
		{
			ListRatio = Convert.ToDouble(DateList.Height) / Convert.ToDouble(MainList.Height + DateList.Height);
			if (!(ListRatio < 1 && ListRatio > 0))
			{
				ListRatio = 0.1;
			}
		}

		private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			ChangedRatio();
			SaveConfig();
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			ChangedSize();
			if (WindowState == FormWindowState.Minimized)
			{
				TriggerMainFormDisplay();
			}
			else
			{
				DefaultState = WindowState;
				SaveConfig();
			}
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			SaveConfig();
		}

		#endregion

		#region 主窗口切换显示隐藏

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void ShowHide_MenuItem_Click(object sender, EventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void Minimize_Button_Click(object sender, EventArgs e)
		{
			TriggerMainFormDisplay();
		}

		private void TriggerMainFormDisplay()
		{
			Visible = !Visible;
			if (Visible)
			{
				if (WindowState == FormWindowState.Minimized)
				{
					WindowState = DefaultState;
				}

				TopMost = true;
				TopMost = false;
			}
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
			PingAll();
		}

		private void StartPing()
		{
			TestAllTimer?.Dispose();
			TestAllTimer = new Timer(StartCore, null, 0, interval);
			Start_Button.Text = I18N.GetString(@"Stop");

			if (Dpi > 1.0)
			{
				Start_Button.ImageScaling = ToolStripItemImageScaling.None;
				Start_Button.Image = Util.Util.ResizeImage(Resources.Stop, DpiPicSize);
			}
			else
			{
				Start_Button.Image = Resources.Stop;
				Start_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			}

			StartStop_MenuItem.Text = I18N.GetString(@"Stop");

			if (SteamManager.UnlockAchievement(@"FIRST_TIME_TCPing"))
			{
				//Console.WriteLine(@"nb");
			}

			SetRichPresence();
		}

		private void StopPing()
		{
			TestAllTimer?.Dispose();
			Start_Button.Text = I18N.GetString(@"Start");

			if (Dpi > 1.0)
			{
				Start_Button.ImageScaling = ToolStripItemImageScaling.None;
				Start_Button.Image = Util.Util.ResizeImage(Resources.Start, DpiPicSize);
			}
			else
			{
				Start_Button.Image = Resources.Start;
				Start_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			}

			StartStop_MenuItem.Text = I18N.GetString(@"Start");

			ClearRichPresence();
		}

		private void TriggerRun()
		{
			Start_Button.Enabled = false;
			StartStop_MenuItem.Enabled = false;

			Task.Run(() =>
			{
				if (Start_Button.Text == I18N.GetString(@"Start"))
				{
					StartPing();
				}
				else
				{
					StopPing();
				}
			}).ContinueWith(task =>
			{
				BeginInvoke(new Action(() =>
				{
					Start_Button.Enabled = true;
					StartStop_MenuItem.Enabled = true;
				}));
			});
		}

		#endregion

		#region 退出程序

		/// <summary>
		/// 关闭前确认
		/// </summary>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (!_isNotifyClose)
				{
					e.Cancel = true;
					TriggerMainFormDisplay();
					return;
				}

				var dr = MessageBox.Show(I18N.GetString(@"If you click ""No"", TCPingInfoView will minimize to Notify Tray"), I18N.GetString(@"Want to Exit?"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					Exit(); //Application.Exit();
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
			else
			{
				Exit();
			}
		}

		private void SaveConfig()
		{
			if (_isLoadSetting)
			{
				if (WindowState == FormWindowState.Normal)
				{
					Config.MainFormHeight = Height;
					Config.MainFormWidth = Width;
					Config.StartPositionLeft = Location.X;
					Config.StartPositionTop = Location.Y;
					Config.DateListHeight = DateList.Height;
					for (var i = 0; i < ColumnsCount; ++i)
					{
						Config.ColumnsOrder[i] = MainList.Columns[i].DisplayIndex;
					}

					for (var i = 0; i < ColumnsCount; ++i)
					{
						if (MainList.Columns[i].Visible)
						{
							Config.ColumnsWidth[i] = MainList.Columns[i].Width;
						}
						else
						{
							Config.ColumnsWidth[i] = 0;
						}
					}
				}
				Config.IsNotifyClose = _isNotifyClose;
				Config.IsShowDateList = _isShowDateList;
				Config.Save();
			}
		}

		private void SaveList()
		{
			Write.WriteToFile(ListPath, RawString);
		}

		private void Exit()
		{
			SaveList();
			if (!Visible)
			{
				TriggerMainFormDisplay();
			}
			SaveConfig();
			Dispose();
			notifyIcon1.Dispose();
			SteamManager.SteamWorksClient.Dispose();
			Environment.Exit(0);
		}

		private void Exit_MenuItem_Click(object sender, EventArgs e)
		{
			Exit();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			Exit();
		}

		#endregion

		#region 文件拖拽进主列表

		private void MainList_DragDrop(object sender, DragEventArgs e)
		{
			var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			RawString = Read.ReadTextFromFile(path);
			rawTable = Read.ReadAddressFromString(RawString);
			LoadFromList();
		}

		private void MainList_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
		}

		#endregion

		#region 加载时间列表

		private void MainList_SelectionChanged(object sender, EventArgs e)
		{
			if (MainList.SelectedRows.Count != 1)
			{
				return;
			}

			if (!DateList.Visible)
			{
				return;
			}

			if (MainList.SelectedRows[0].Cells[0].Value is int index)
			{
				LoadLogs(index);
			}
		}

		private void LoadLogs(DateTable log)
		{
			try
			{
				DateTable.Add(log);
				if (DateList.SelectedRows.Count == 0)
				{
					DateList.Rows[0].Selected = true;
				}
			}
			catch
			{
				// ignored
			}
		}

		private void LoadLogs(int index)
		{
			try
			{
				if (index <= 0)
				{
					DateTable.Clear();
				}
				else
				{
					DateTable.Clear();
					dateTable = (ConcurrentList<DateTable>)mainTable[index - 1].Info;
					ToLogs(dateTable);
					if (dateTable.Count > 0)
					{
						DateList.Rows[0].Selected = true;
					}
				}
			}
			catch
			{
				// ignored
			}
		}

		private void ToLogs(IEnumerable<DateTable> table)
		{
			foreach (var item in table)
			{
				DateTable.Add(item);
			}
		}

		#endregion

		#region 鼠标事件

		/// <summary>
		/// 点击空白处清空时间列表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void List_MouseDown(object sender, MouseEventArgs e)
		{
			var dgv = (DataGridView)sender;

			var rowIndex = dgv.HitTest(e.X, e.Y).RowIndex;

			if (rowIndex == -1)
			{
				dgv.ClearSelection();
				if (dgv == MainList)
				{
					LoadLogs(-1);
				}
			}
		}

		private void MainList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				if (MainList.Rows[e.RowIndex].Cells[0].Value is int index)
				{
					var log = mainTable[index - 1];
					new LogForm(log).ShowDialog();
				}
			}
		}

		private void MainList_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_mainListMouseLocationX = e.X;
				_mainListMouseLocationY = e.Y;
			}
		}

		#endregion

		#region 列表单元格内容改变

		private void MainList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 4)
			{
				var value = MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as int?;
				var cell = MainList.Rows[e.RowIndex].Cells[1] as TextAndImageCell;
				if (value != null)
				{
					if (value < HighLatency)
					{
						MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = LowLatencyColor;
						cell.Image = imageList1.Images[0];
					}
					else if (value < Timeout)
					{
						MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = HighLatencyColor;
						cell.Image = imageList1.Images[0];
					}
					else
					{
						MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = TimeoutColor;
						cell.Image = imageList1.Images[1];
					}
				}
				else
				{
					cell.Image = imageList1.Images[2];
				}
			}
			else if (e.ColumnIndex == 6 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
			{
				if (MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value is int value)
				{
					if (value < HighLatency)
					{
						MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = LowLatencyColor;
					}
					else if (value < Timeout)
					{
						MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = HighLatencyColor;
					}
					else
					{
						MainList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = TimeoutColor;
					}
				}
			}
		}

		private void DateList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				var value = DateList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as int?;
				var cell = DateList.Rows[e.RowIndex].Cells[0] as TextAndImageCell;
				if (value < HighLatency)
				{
					DateList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = LowLatencyColor;

					cell.Image = imageList1.Images[0];
				}
				else if (value < Timeout)
				{
					DateList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = HighLatencyColor;

					cell.Image = imageList1.Images[0];
				}
				else
				{
					DateList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = TimeoutColor;

					cell.Image = imageList1.Images[1];
				}
			}
		}

		#endregion

		#region 列表进入/失去焦点

		private void MainList_Leave(object sender, EventArgs e)
		{
			MainList.DefaultCellStyle.SelectionBackColor = SystemColors.InactiveCaption;
			MainList.DefaultCellStyle.SelectionForeColor = SystemColors.InactiveCaptionText;
		}

		private void MainList_Enter(object sender, EventArgs e)
		{
			MainList.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			MainList.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
			DateList.DefaultCellStyle.SelectionBackColor = SystemColors.InactiveCaption;
			DateList.DefaultCellStyle.SelectionForeColor = SystemColors.InactiveCaptionText;
		}

		private void DateList_Enter(object sender, EventArgs e)
		{
			DateList.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			DateList.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
		}

		private void DateList_Leave(object sender, EventArgs e)
		{
			DateList.DefaultCellStyle.SelectionBackColor = SystemColors.InactiveCaption;
			DateList.DefaultCellStyle.SelectionForeColor = SystemColors.InactiveCaptionText;
		}

		#endregion

		#region 选项

		private void IsNotifyClose_MenuItem_Click(object sender, EventArgs e)
		{
			IsNotifyClose_MenuItem.Checked = !IsNotifyClose_MenuItem.Checked;
		}

		private void IsNotifyClose_MenuItem_CheckedChanged(object sender, EventArgs e)
		{
			_isNotifyClose = IsNotifyClose_MenuItem.Checked;
			SaveConfig();
		}

		private void IsShowDateList_MenuItem_Click(object sender, EventArgs e)
		{
			IsShowDateList_MenuItem.Checked = !IsShowDateList_MenuItem.Checked;
		}

		private void IsShowDateList_MenuItem_CheckStateChanged(object sender, EventArgs e)
		{
			_isShowDateList = IsShowDateList_MenuItem.Checked;
			SaveConfig();
			splitter1.Visible = _isShowDateList;
			DateList.Visible = _isShowDateList;
			if (DateList.Visible)
			{
				if (MainList.SelectedRows.Count == 1)
				{
					if (MainList.SelectedRows[0].Cells[0].Value is int index)
					{
						LoadLogs(index);
					}
				}
			}
		}

		private TCPingOptions GetTCPingOptions()
		{
			return new TCPingOptions
			{
				Title = UserTitle,
				ReverseDnsTimeout = Convert.ToInt32(ReverseDNSTimeout),
				Timeout = Convert.ToInt32(Timeout),
				HighLatency = Convert.ToInt32(HighLatency),
				TCPingInterval = interval / second,//ms -> s
				TimeoutColor = TimeoutColor,
				HighLatencyColor = HighLatencyColor,
				LowLatencyColor = LowLatencyColor
			};
		}

		private void SetTCPingOptions(TCPingOptions setting, string list)
		{
			RawString = list;

			LoadTCPingOptions(setting);

			Config.TCPingOptions = setting;

			LoadTitle();
			rawTable = Read.ReadAddressFromString(RawString);
			LoadFromList();
		}

		private void List_Button_Click(object sender, EventArgs e)
		{
			var form = new TCPingOptionsForm(GetTCPingOptions(), RawString);
			if (form.ShowDialog() == DialogResult.OK)
			{
				SetTCPingOptions(form.Setting, form.List);
				Config.TCPingOptions = form.Setting;
				SaveConfig();
			}
		}

		#endregion

		#region 查看

		private void AutoColumnSize_MenuItem_Click(object sender, EventArgs e)
		{
			MainList.ColumnWidthChanged -= MainList_ColumnWidthChanged;
			Util.Util.AutoColumnSize(MainList, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
			SaveConfig();
			MainList.ColumnWidthChanged += MainList_ColumnWidthChanged;
		}

		private void AutoColumnsSizeAndHeader_MenuItem_Click(object sender, EventArgs e)
		{
			MainList.ColumnWidthChanged -= MainList_ColumnWidthChanged;
			Util.Util.AutoColumnSize(MainList, DataGridViewAutoSizeColumnMode.AllCells);
			SaveConfig();
			MainList.ColumnWidthChanged += MainList_ColumnWidthChanged;
		}

		private void DisplayedColumns_MenuItem_Click(object sender, EventArgs e)
		{
			var d = new DisplayedColumns(MainList.Columns);
			d.AfterColumnsChanged += AfterColumnsChanged;
			d.ShowDialog();
		}

		private void ShowLogForm_MenuItem_Click(object sender, EventArgs e)
		{
			var i = MainList.SelectedRows.Count;
			if (i == 1)
			{
				if (MainList.Rows[MainList.SelectedRows[0].Index].Cells[0].Value is int index)
				{
					var log = mainTable[index - 1];
					new LogForm(log).ShowDialog();
				}
			}
		}

		#endregion

		#region 文件

		private void Reset_MenuItem_Click(object sender, EventArgs e)
		{
			StopPing();
			cts_PingTask.Cancel();
			Util.Util.RemoveCompletedTasks(ref PingTasks);
			Task.WaitAll(PingTasks.ToArray());
			cts_PingTask.Dispose();
			cts_PingTask = new CancellationTokenSource();
			PingTasks = new ConcurrentList<Task>();
			foreach (var log in mainTable)
			{
				log.Reset();
			}

			MainTable.Clear();
			DateTable.Clear();

			ToMainTable(mainTable);
			if (rawTable.Count > 0)
			{
				MainList.Rows[0].Selected = true;
			}
		}

		#endregion

		#region 帮助

		private void About_MenuItem_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}

		#endregion

		#region 搜索

		private bool IsContainsString(int rowIndex, string str)
		{
			for (var i = 0; i < MainList.ColumnCount; ++i)
			{
				if (MainList.Columns[i].Visible)
				{
					var value = MainList.Rows[rowIndex].Cells[i].Value;
					if (value != null)
					{
						var s = MainList.Rows[rowIndex].Cells[i].Value.ToString();
						if (!string.IsNullOrWhiteSpace(s) && s.Contains(str))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		private void SearchMainList()
		{
			var index = 0;
			if (MainList.SelectedRows.Count == 1)
			{
				index = MainList.SelectedRows[0].Index;
			}

			for (var i = 0; i < MainList.RowCount + 1; ++i)
			{
				if (IsContainsString(index, Search_TextBox.Text))
				{
					if (!MainList.Rows[index].Selected)
					{
						MainList.Rows[index].Selected = true;
						MainList.CurrentCell = MainList.Rows[index].Cells[0];
						return;
					}
					MainList.ClearSelection();
				}

				if (index == MainList.RowCount - 1)
				{
					index = 0;
				}
				else
				{
					++index;
				}
			}

			MainList.ClearSelection();
		}

		private void SearchTextBox_TextChanged(object sender, EventArgs e)
		{
			SearchMainList();
		}

		private void Search_TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Enter))
			{
				SearchMainList();
				e.Handled = true;
			}
		}

		#endregion

		#region 主列表顺序、显示列、列宽改变

		private void MainList_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
		{
			Debug.WriteLine(@"主列表顺序");
			SaveConfig();
		}

		private void AfterColumnsChanged(object sender, EventArgs e)
		{
			Debug.WriteLine(@"显示列改变");
			SaveConfig();
		}

		private void MainList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			var columnIndex = MainList.HitTest(_mainListMouseLocationX, _mainListMouseLocationY).ColumnIndex;
			if (e.Column.Index == columnIndex)
			{
				Debug.WriteLine($@"列宽改变:{columnIndex}");
				SaveConfig();
			}
		}

		#endregion

		#region Steam RichPresence

		private void SetRichPresence()
		{
			if (string.IsNullOrWhiteSpace(UserTitle))
			{
				SteamManager.SetStatus(mainTable.Count);
			}
			else
			{
				SteamManager.SetCustomString(mainTable.Count, UserTitle);
			}
		}

		private void ClearRichPresence()
		{
			SteamManager.ClearRichPresence();
		}

		#endregion
	}
}
