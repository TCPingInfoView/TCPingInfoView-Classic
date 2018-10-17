using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

		#region 超时设置

		public static int ReverseDNSTimeout = 1000;
		public static int Timeout = 3000;
		public static int HighLatency = 300;
		public static Color TimeoutColor = Color.Red;
		public static Color HighLatencyColor = Color.Coral;
		public static Color LowLatencyColor = Color.Green;

		#endregion

		private int Needheight;
		private double listViewsProportion;

		private readonly BindingCollection<MainTable> Maintable = new BindingCollection<MainTable>();
		private ConcurrentList<MainTable> maintable = new ConcurrentList<MainTable>();
		private ConcurrentList<Data> rawtable = new ConcurrentList<Data>();


		private ConcurrentList<Task> PingTasks = new ConcurrentList<Task>();
		private static CancellationTokenSource cts_PingTask = new CancellationTokenSource();

		private readonly BindingCollection<DateTable> Datetable = new BindingCollection<DateTable>();
		private ConcurrentList<DateTable> datetable = new ConcurrentList<DateTable>();

		private ConcurrentList<TCPingLog> logs = new ConcurrentList<TCPingLog>();

		private delegate void VoidMethodDelegate();

		#region Timer

		private Timer TestAllTimer;
		private const int second = 1000;
		private const int minute = 60 * second;
		public int interval = 1 * minute;

		#endregion

		#region 窗口第一次载入

		private void LoadMainlistView()
		{
			MainlistView.Columns[0].HeaderText = @"列表顺序";
			MainlistView.Columns[1].HeaderText = @"主机名";
			MainlistView.Columns[2].HeaderText = @"IP:端口";
			MainlistView.Columns[3].HeaderText = @"失败率";
			MainlistView.Columns[4].HeaderText = @"延迟(ms)";
			MainlistView.Columns[5].HeaderText = @"说明";

			MainlistView.Columns[0].DataPropertyName = @"Index";
			MainlistView.Columns[1].DataPropertyName = @"HostsName";
			MainlistView.Columns[2].DataPropertyName = @"Endpoint";
			MainlistView.Columns[3].DataPropertyName = @"FailedP";
			MainlistView.Columns[4].DataPropertyName = @"Latency";
			MainlistView.Columns[5].DataPropertyName = @"Description";
		}

		private void LoadDatelistView()
		{
			DatelistView.Columns[0].HeaderText = @"TCPing 通信时间";
			DatelistView.Columns[1].HeaderText = @"延迟(ms)";

			DatelistView.Columns[0].DataPropertyName = @"Date";
			DatelistView.Columns[1].DataPropertyName = @"Latenty";
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Needheight = Height - (MainlistView.Height + DatelistView.Height);
			listViewsProportion = Convert.ToDouble(MainlistView.Height) / Convert.ToDouble(MainlistView.Height + DatelistView.Height);

			MainlistView.AutoGenerateColumns = false;
			MainlistView.DataSource = Maintable;
			LoadMainlistView();

			DatelistView.AutoGenerateColumns = false;
			DatelistView.DataSource = Datetable;
			LoadDatelistView();

			const string defaultPath = @".\test.txt";
			if (File.Exists(defaultPath))
			{
				rawtable = Read.ReadAddressFromFile(defaultPath);
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

			rawtable = Read.ReadAddressFromFile(path);
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
				if (Maintable.All(x => x.Index != item.Index))
				{
					Maintable.Add(item);
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
			Util.RemoveCompletedTasks(ref PingTasks);
			Task.WaitAll(PingTasks.ToArray());
			cts_PingTask.Dispose();
			cts_PingTask = new CancellationTokenSource();
			PingTasks = new ConcurrentList<Task>();

			Maintable.Clear();
			Datetable.Clear();

			logs = new ConcurrentList<TCPingLog>();
			for (var i = 0; i < rawtable.Count; ++i)
			{
				logs.Add(new TCPingLog());
			}

			maintable = Util.ToMainTable(rawtable);
			ToMainTable(maintable);
			if (rawtable.Count > 0)
			{
				MainlistView.Rows[0].Selected = true;
			}
			FirstPing();
		}

		#endregion

		#region TCPing 核心

		private void FirstPing()
		{
			var t = new Task(() =>
			{
				Parallel.For(0, maintable.Count, (i, state) =>
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

					if (Util.IsIPv4Address(maintable[i].HostsName)) //反查DNS
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

						maintable[i].HostsName = NetTest.GetHostName(IPAddress.Parse(maintable[i].HostsName), ReverseDNSTimeout);
					}
					else
					{
						var ip = NetTest.GetIP(maintable[i].HostsName);

						try
						{
							cts_PingTask.Token.ThrowIfCancellationRequested();
						}
						catch (OperationCanceledException)
						{
							state.Stop();
							return;
						}

						maintable[i].Endpoint = $@"{ip}:{rawtable[i].Port}";
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
			if (maintable[index].Endpoint != string.Empty)
			{
				var ipe = Util.ToIPEndPoint(maintable[index].Endpoint, 443);
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
					Latenty = res
				};
				logs[index].Add(log);

				var fp = logs[index].FailedP;
				var fpStr = fp > 0.0 ? fp.ToString(@"P") : @"0%";

				maintable[index].FailedP = fpStr;
				maintable[index].Latency = res;

				if (MainlistView.SelectedRows.Count > 0)
				{
					var i = MainlistView.SelectedRows[0].Cells[0].Value as int?;
					if (i == index)
					{
						DatelistView.Invoke(() => { LoadLogs(index); });
					}
				}
			}
		}

		private void PingAll()
		{
			var t = new Task(() =>
			{
				Parallel.For(0, maintable.Count, (i, state) =>
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
			Util.RemoveCompletedTasks(ref PingTasks);
			PingTasks.Add(t);
			t.Start();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			PingAll();
		}

		#endregion

		#region 保持两表格的比例(待实现)

		private void ChangeSize()
		{
			var height = Height - Needheight;
			MainlistView.Height = Convert.ToInt32(listViewsProportion * height);
			DatelistView.Height = height - MainlistView.Height;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			//ChangeSize();
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

		private void StartCore(object state)
		{
			PingAll();
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

				var dr = MessageBox.Show(@"「是」退出，「否」最小化", @"是否退出？", MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					Dispose();
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
		}

		private void Exit()
		{
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

		private void MainlistView_DragDrop(object sender, DragEventArgs e)
		{
			var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			rawtable = Read.ReadAddressFromFile(path);
			LoadFromList();
		}

		private void MainlistView_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
		}

		#endregion

		#region 加载时间列表

		private void MainlistView_SelectionChanged(object sender, EventArgs e)
		{
			if (MainlistView.SelectedRows.Count <= 0)
			{
				return;
			}

			if (MainlistView.SelectedRows[0].Cells[0].Value is int index)
			{
				LoadLogs(index);
			}
		}

		private void LoadLogs(int index)
		{
			if (index <= 0)
			{
				Datetable.Clear();
			}
			else
			{
				Datetable.Clear();
				datetable = (ConcurrentList<DateTable>)logs[index - 1].Info;
				ToLogs(datetable);
				if (datetable.Count > 0)
				{
					DatelistView.Rows[0].Selected = true;
				}
			}
		}

		private void ToLogs(IEnumerable<DateTable> table)
		{
			foreach (var item in table)
			{
				Datetable.Add(item);
			}
		}

		#endregion

		#region 鼠标点击事件

		/// <summary>
		/// 点击空白处清空时间列表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListView_MouseDown(object sender, MouseEventArgs e)
		{
			var dgv = (DataGridView)sender;

			var rowIndex = dgv.HitTest(e.X, e.Y).RowIndex;

			if (rowIndex == -1)
			{
				dgv.ClearSelection();
				if (dgv == MainlistView)
				{
					LoadLogs(-1);
				}
			}
		}

		#endregion

		#region 列表单元格内容改变

		private void MainlistView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 4)
			{
				var value = MainlistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as int?;
				var cell = MainlistView.Rows[e.RowIndex].Cells[1] as TextAndImageCell;
				if (value < HighLatency)
				{
					MainlistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = LowLatencyColor;

					cell.Image = imageList1.Images[0];
				}
				else if (value < Timeout)
				{
					MainlistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = HighLatencyColor;

					cell.Image = imageList1.Images[0];
				}
				else
				{
					MainlistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = TimeoutColor;

					cell.Image = imageList1.Images[1];
				}
			}
		}

		private void DatelistView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				var value = DatelistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as int?;
				var cell = DatelistView.Rows[e.RowIndex].Cells[0] as TextAndImageCell;
				if (value < HighLatency)
				{
					DatelistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = LowLatencyColor;

					cell.Image = imageList1.Images[0];
				}
				else if (value < Timeout)
				{
					DatelistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = HighLatencyColor;

					cell.Image = imageList1.Images[0];
				}
				else
				{
					DatelistView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = TimeoutColor;

					cell.Image = imageList1.Images[1];
				}
			}
		}

		#endregion

		#region 列表进入/失去焦点

		private void MainlistView_Leave(object sender, EventArgs e)
		{
			MainlistView.DefaultCellStyle.SelectionBackColor = SystemColors.InactiveCaption;
			MainlistView.DefaultCellStyle.SelectionForeColor = SystemColors.InactiveCaptionText;
		}

		private void MainlistView_Enter(object sender, EventArgs e)
		{
			MainlistView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			MainlistView.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
			DatelistView.DefaultCellStyle.SelectionBackColor = SystemColors.InactiveCaption;
			DatelistView.DefaultCellStyle.SelectionForeColor = SystemColors.InactiveCaptionText;
		}

		private void DatelistView_Enter(object sender, EventArgs e)
		{
			DatelistView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			DatelistView.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
		}

		private void DatelistView_Leave(object sender, EventArgs e)
		{
			DatelistView.DefaultCellStyle.SelectionBackColor = SystemColors.InactiveCaption;
			DatelistView.DefaultCellStyle.SelectionForeColor = SystemColors.InactiveCaptionText;
		}

		#endregion

	}
}
