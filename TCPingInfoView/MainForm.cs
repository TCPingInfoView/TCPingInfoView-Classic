using System;
using System.Collections.Generic;
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
using TCPingInfoView.NetUtils;
using TCPingInfoView.Properties;
using TCPingInfoView.Util;
using Timer = System.Threading.Timer;

namespace TCPingInfoView
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			Icon = Resources.TCPing;
			notifyIcon1.Icon = Resources.TCPing_White;
			Config.Load();
		}

		private static string ExeName => Assembly.GetExecutingAssembly().GetName().Name;
		private readonly AppConfig Config = new AppConfig($@".\{ExeName}.json");
		private static string ListPath => $@".\{ExeName}.txt";

		#region DPI

		private double Dpi => this.GetDpi();
		private static Size DefPicSize => new Size(16, 16);
		private Size DpiPicSize => new Size(Convert.ToInt32(DefPicSize.Width * Dpi), Convert.ToInt32(DefPicSize.Height * Dpi));

		#endregion

		#region 表格显示参数

		private int RemainingHeight;
		private double ListRatio;
		public const int ColumnsCount = 12;

		#endregion

		#region 杂项设置

		private bool _isNotifyClose;
		private bool _isShowDateList;

		#endregion

		#region 超时设置

		public static int ReverseDNSTimeout = 2000;
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

		#region 委托

		private delegate void VoidMethodDelegate();

		#endregion

		#region Timer

		private Timer TestAllTimer;
		private const int second = 1000;
		private const int minute = 60 * second;
		public int interval = 1 * minute;

		#endregion

		#region 窗口第一次载入

		private void LoadMainList()
		{
			MainList.Columns[0].HeaderText = @"列表顺序";
			MainList.Columns[1].HeaderText = @"主机名";
			MainList.Columns[2].HeaderText = @"IP:端口";
			MainList.Columns[3].HeaderText = @"失败率";
			MainList.Columns[4].HeaderText = @"延迟(ms)";
			MainList.Columns[5].HeaderText = @"说明";
			MainList.Columns[6].HeaderText = @"平均用时(ms)";
			MainList.Columns[7].HeaderText = @"成功率";
			MainList.Columns[8].HeaderText = @"成功次数";
			MainList.Columns[9].HeaderText = @"失败次数";
			MainList.Columns[10].HeaderText = @"最长用时(ms)";
			MainList.Columns[11].HeaderText = @"最短用时(ms)";

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
			DateList.Columns[0].HeaderText = @"TCPing 通信时间";
			DateList.Columns[1].HeaderText = @"延迟(ms)";

			DateList.Columns[0].DataPropertyName = @"Date";
			DateList.Columns[1].DataPropertyName = @"Latency";
		}

		private void LoadButtons()
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
			}
			else
			{
				Test_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Start_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Exit_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
				Load_Button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			}
		}

		private void LoadSetting()
		{
			Height = Config.MainFormHeight;
			Width = Config.MainFormWidth;
			DateList.Height = Config.DateListHeight;

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
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			LoadSetting();

			RemainingHeight = Height - (MainList.Height + DateList.Height);
			ChangedRatio();

			LoadButtons();

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
					try
					{
						cts_PingTask.Token.ThrowIfCancellationRequested();
					}
					catch (OperationCanceledException)
					{
						state.Stop();
						return;
					}

					if (IPFormatter.IsIPv4Address(mainTable[i].HostsName)) //反查DNS
					{
						PingOne(i);

						try
						{
							cts_PingTask.Token.ThrowIfCancellationRequested();
						}
						catch (OperationCanceledException)
						{
							state.Stop();
							return;
						}

						mainTable[i].HostsName = NetTest.GetHostName(IPAddress.Parse(mainTable[i].HostsName), ReverseDNSTimeout);
					}
					else
					{
						var ip = NetTest.GetIP(mainTable[i].HostsName);

						try
						{
							cts_PingTask.Token.ThrowIfCancellationRequested();
						}
						catch (OperationCanceledException)
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

					try
					{
						cts_PingTask.Token.ThrowIfCancellationRequested();
					}
					catch (OperationCanceledException)
					{
						state.Stop();
					}
				});
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

			var log = new DateTable
			{
				Date = time,
				Latency = res
			};

			mainTable[index].AddNewLog(log);

			if (MainList.SelectedRows.Count > 0)
			{
				var i = MainList.SelectedRows[0].Cells[0].Value as int?;
				if (i == index)
				{
					DateList.Invoke(() => { LoadLogs(index); });
				}
			}
		}

		private void PingAll()
		{
			var t = new Task(() =>
			{
				Parallel.For(0, mainTable.Count, (i, state) =>
				{
					try
					{
						cts_PingTask.Token.ThrowIfCancellationRequested();
					}
					catch (OperationCanceledException)
					{
						state.Stop();
						return;
					}

					PingOne(i);

					try
					{
						cts_PingTask.Token.ThrowIfCancellationRequested();
					}
					catch (OperationCanceledException)
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
			var height = Height - RemainingHeight;
			DateList.Height = Convert.ToInt32(ListRatio * height);
		}

		private void ChangedRatio()
		{
			ListRatio = Convert.ToDouble(DateList.Height) / Convert.ToDouble(MainList.Height + DateList.Height);
		}

		private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			ChangedRatio();
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			ChangedSize();
		}

		#endregion

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

		private void StartStop_MenuItem2_Click(object sender, EventArgs e)
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
			Start_Button.Text = @"停止";

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

			StartStop_MenuItem.Text = @"停止";
			StartStop_MenuItem2.Text = @"停止";
		}

		private void StopPing()
		{
			TestAllTimer?.Dispose();
			Start_Button.Text = @"开始";

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

			StartStop_MenuItem.Text = @"开始";
			StartStop_MenuItem2.Text = @"开始";
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

		/// <summary>
		/// 关闭前是否确认
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

				var dr = MessageBox.Show(@"「是」退出，「否」最小化", @"是否退出？", MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Question);
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
			Config.MainFormHeight = Height;
			Config.MainFormWidth = Width;
			Config.DateListHeight = DateList.Height;
			Config.IsNotifyClose = _isNotifyClose;
			Config.IsShowDateList = _isShowDateList;
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
			Config.Save();
		}

		private void SaveList()
		{
			Write.WriteToFile(ListPath, RawString);
		}

		private void Exit()
		{
			SaveList();
			SaveConfig();
			Dispose();
			notifyIcon1.Dispose();
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

		private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
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
			if (MainList.SelectedRows.Count <= 0)
			{
				return;
			}

			if (MainList.SelectedRows[0].Cells[0].Value is int index)
			{
				LoadLogs(index);
			}
		}

		private void LoadLogs(int index)
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

		private void ToLogs(IEnumerable<DateTable> table)
		{
			foreach (var item in table)
			{
				DateTable.Add(item);
			}
		}

		#endregion

		#region 鼠标点击事件

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
					var logForm = new LogForm(log);
					logForm.ShowDialog();
				}
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
		}

		private void IsShowDateList_MenuItem_Click(object sender, EventArgs e)
		{
			IsShowDateList_MenuItem.Checked = !IsShowDateList_MenuItem.Checked;
		}

		private void IsShowDateList_MenuItem_CheckStateChanged(object sender, EventArgs e)
		{
			_isShowDateList = IsShowDateList_MenuItem.Checked;
			splitter1.Visible = _isShowDateList;
			DateList.Visible = _isShowDateList;
		}

		#endregion

		#region 查看

		private void AutoColumnSize_MenuItem_Click(object sender, EventArgs e)
		{
			Util.Util.AutoColumnSize(MainList, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
		}

		private void AutoColumnsSizeAndHeader_MenuItem_Click(object sender, EventArgs e)
		{
			Util.Util.AutoColumnSize(MainList, DataGridViewAutoSizeColumnMode.AllCells);
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
			var aboutForm=new AboutForm();
			aboutForm.ShowDialog();
		}

		#endregion

	}
}
