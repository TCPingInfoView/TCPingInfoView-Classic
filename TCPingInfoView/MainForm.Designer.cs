using System.ComponentModel;
using System.Windows.Forms;
using TCPingInfoView.Control;

namespace TCPingInfoView
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.File_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.View_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Options_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.IsNotifyClose_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.IsShowDateList_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Help_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.About_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.NotifyIcon_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ShowHide_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StartStop_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.Exit_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Latency2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.Load_Button = new System.Windows.Forms.ToolStripButton();
			this.Test_Button = new System.Windows.Forms.ToolStripButton();
			this.Start_Button = new System.Windows.Forms.ToolStripButton();
			this.Exit_Button = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.MainList = new TCPingInfoView.Control.DoubleBufferDataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new TCPingInfoView.Control.TextAndImageColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MainList_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DateList = new TCPingInfoView.Control.DoubleBufferDataGridView();
			this.dataGridViewTextBoxColumn1 = new TCPingInfoView.Control.TextAndImageColumn();
			this.textAndImageColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AutoColumnsSize_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AutoColumnsSizeAndHeader_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DisplayedColumns_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShowLogForm_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.Reset_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LoadFile_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.NotifyIcon_MenuStrip.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainList)).BeginInit();
			this.MainList_MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DateList)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_MenuItem,
            this.View_MenuItem,
            this.Options_MenuItem,
            this.Help_MenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(912, 25);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// File_MenuItem
			// 
			this.File_MenuItem.DropDown = this.NotifyIcon_MenuStrip;
			this.File_MenuItem.Name = "File_MenuItem";
			this.File_MenuItem.Size = new System.Drawing.Size(58, 21);
			this.File_MenuItem.Text = "文件(&F)";
			// 
			// View_MenuItem
			// 
			this.View_MenuItem.DropDown = this.MainList_MenuStrip;
			this.View_MenuItem.Name = "View_MenuItem";
			this.View_MenuItem.Size = new System.Drawing.Size(60, 21);
			this.View_MenuItem.Text = "查看(&V)";
			// 
			// Options_MenuItem
			// 
			this.Options_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IsNotifyClose_MenuItem,
            this.IsShowDateList_MenuItem});
			this.Options_MenuItem.Name = "Options_MenuItem";
			this.Options_MenuItem.Size = new System.Drawing.Size(62, 21);
			this.Options_MenuItem.Text = "选项(&O)";
			// 
			// IsNotifyClose_MenuItem
			// 
			this.IsNotifyClose_MenuItem.Name = "IsNotifyClose_MenuItem";
			this.IsNotifyClose_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.IsNotifyClose_MenuItem.Text = "关闭时提示";
			this.IsNotifyClose_MenuItem.CheckedChanged += new System.EventHandler(this.IsNotifyClose_MenuItem_CheckedChanged);
			this.IsNotifyClose_MenuItem.Click += new System.EventHandler(this.IsNotifyClose_MenuItem_Click);
			// 
			// IsShowDateList_MenuItem
			// 
			this.IsShowDateList_MenuItem.Checked = true;
			this.IsShowDateList_MenuItem.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.IsShowDateList_MenuItem.Name = "IsShowDateList_MenuItem";
			this.IsShowDateList_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.IsShowDateList_MenuItem.Text = "显示日期表格";
			this.IsShowDateList_MenuItem.CheckStateChanged += new System.EventHandler(this.IsShowDateList_MenuItem_CheckStateChanged);
			this.IsShowDateList_MenuItem.Click += new System.EventHandler(this.IsShowDateList_MenuItem_Click);
			// 
			// Help_MenuItem
			// 
			this.Help_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.About_MenuItem});
			this.Help_MenuItem.Name = "Help_MenuItem";
			this.Help_MenuItem.Size = new System.Drawing.Size(61, 21);
			this.Help_MenuItem.Text = "帮助(&H)";
			// 
			// About_MenuItem
			// 
			this.About_MenuItem.Name = "About_MenuItem";
			this.About_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.About_MenuItem.Text = "关于";
			this.About_MenuItem.Click += new System.EventHandler(this.About_MenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 656);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(912, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(277, 17);
			this.toolStripStatusLabel1.Text = "https://github.com/HMBSbige/TCPingInfoView";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.NotifyIcon_MenuStrip;
			this.notifyIcon1.Text = "TCPingInfoView";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// NotifyIcon_MenuStrip
			// 
			this.NotifyIcon_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowHide_MenuItem,
            this.LoadFile_MenuItem,
            this.toolStripSeparator1,
            this.StartStop_MenuItem,
            this.Reset_MenuItem,
            this.toolStripSeparator2,
            this.Exit_MenuItem});
			this.NotifyIcon_MenuStrip.Name = "NotifyIcon_MenuStrip";
			this.NotifyIcon_MenuStrip.Size = new System.Drawing.Size(181, 148);
			// 
			// ShowHide_MenuItem
			// 
			this.ShowHide_MenuItem.Name = "ShowHide_MenuItem";
			this.ShowHide_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.ShowHide_MenuItem.Text = "显示/隐藏";
			this.ShowHide_MenuItem.Click += new System.EventHandler(this.ShowHide_MenuItem_Click);
			// 
			// StartStop_MenuItem
			// 
			this.StartStop_MenuItem.Name = "StartStop_MenuItem";
			this.StartStop_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.StartStop_MenuItem.Text = "开始";
			this.StartStop_MenuItem.Click += new System.EventHandler(this.StartStop_MenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// Exit_MenuItem
			// 
			this.Exit_MenuItem.Name = "Exit_MenuItem";
			this.Exit_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.Exit_MenuItem.Text = "退出";
			this.Exit_MenuItem.Click += new System.EventHandler(this.Exit_MenuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Succeed.png");
			this.imageList1.Images.SetKeyName(1, "Failed.png");
			this.imageList1.Images.SetKeyName(2, "None.png");
			// 
			// Date
			// 
			this.Date.Text = "TCPing 通信时间";
			this.Date.Width = 170;
			// 
			// Latency2
			// 
			this.Latency2.Text = "延迟(ms)";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "列表顺序";
			this.columnHeader1.Width = 68;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "主机名";
			this.columnHeader2.Width = 129;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "IP:端口";
			this.columnHeader3.Width = 95;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "失败率";
			this.columnHeader4.Width = 54;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "延迟(ms)";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "说明";
			this.columnHeader6.Width = 141;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 528);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(912, 3);
			this.splitter1.TabIndex = 7;
			this.splitter1.TabStop = false;
			this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
			// 
			// Load_Button
			// 
			this.Load_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Load_Button.Image = global::TCPingInfoView.Properties.Resources.Load;
			this.Load_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Load_Button.Name = "Load_Button";
			this.Load_Button.Size = new System.Drawing.Size(23, 22);
			this.Load_Button.Text = "载入";
			this.Load_Button.Click += new System.EventHandler(this.toolStripButton3_Click);
			// 
			// Test_Button
			// 
			this.Test_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Test_Button.Image = global::TCPingInfoView.Properties.Resources.Test;
			this.Test_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Test_Button.Name = "Test_Button";
			this.Test_Button.Size = new System.Drawing.Size(23, 22);
			this.Test_Button.Text = "测试";
			this.Test_Button.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// Start_Button
			// 
			this.Start_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Start_Button.Image = global::TCPingInfoView.Properties.Resources.Start;
			this.Start_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Start_Button.Name = "Start_Button";
			this.Start_Button.Size = new System.Drawing.Size(23, 22);
			this.Start_Button.Text = "开始";
			this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
			// 
			// Exit_Button
			// 
			this.Exit_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Exit_Button.Image = global::TCPingInfoView.Properties.Resources.Exit;
			this.Exit_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Exit_Button.Name = "Exit_Button";
			this.Exit_Button.Size = new System.Drawing.Size(23, 22);
			this.Exit_Button.Text = "退出";
			this.Exit_Button.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Load_Button,
            this.toolStripSeparator5,
            this.Test_Button,
            this.Start_Button,
            this.toolStripSeparator4,
            this.Exit_Button});
			this.toolStrip1.Location = new System.Drawing.Point(0, 25);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(912, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// MainList
			// 
			this.MainList.AllowDrop = true;
			this.MainList.AllowUserToAddRows = false;
			this.MainList.AllowUserToDeleteRows = false;
			this.MainList.AllowUserToOrderColumns = true;
			this.MainList.AllowUserToResizeRows = false;
			this.MainList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.MainList.BackgroundColor = System.Drawing.SystemColors.Window;
			this.MainList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.MainList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.MainList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.MainList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MainList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
			this.MainList.ContextMenuStrip = this.MainList_MenuStrip;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.MainList.DefaultCellStyle = dataGridViewCellStyle7;
			this.MainList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainList.GridColor = System.Drawing.SystemColors.Control;
			this.MainList.Location = new System.Drawing.Point(0, 50);
			this.MainList.MultiSelect = false;
			this.MainList.Name = "MainList";
			this.MainList.ReadOnly = true;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.MainList.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.MainList.RowHeadersVisible = false;
			this.MainList.Size = new System.Drawing.Size(912, 478);
			this.MainList.TabIndex = 6;
			this.MainList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainList_CellDoubleClick);
			this.MainList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MainList_CellFormatting);
			this.MainList.SelectionChanged += new System.EventHandler(this.MainList_SelectionChanged);
			this.MainList.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainList_DragDrop);
			this.MainList.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainList_DragEnter);
			this.MainList.Enter += new System.EventHandler(this.MainList_Enter);
			this.MainList.Leave += new System.EventHandler(this.MainList_Leave);
			this.MainList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.List_MouseDown);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Column2";
			this.Column2.Image = null;
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Column3";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Column4";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "Column5";
			this.Column5.Name = "Column5";
			this.Column5.ReadOnly = true;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "Column6";
			this.Column6.Name = "Column6";
			this.Column6.ReadOnly = true;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "Column7";
			this.Column7.Name = "Column7";
			this.Column7.ReadOnly = true;
			// 
			// Column8
			// 
			this.Column8.HeaderText = "Column8";
			this.Column8.Name = "Column8";
			this.Column8.ReadOnly = true;
			// 
			// Column9
			// 
			this.Column9.HeaderText = "Column9";
			this.Column9.Name = "Column9";
			this.Column9.ReadOnly = true;
			// 
			// Column10
			// 
			this.Column10.HeaderText = "Column10";
			this.Column10.Name = "Column10";
			this.Column10.ReadOnly = true;
			// 
			// Column11
			// 
			this.Column11.HeaderText = "Column11";
			this.Column11.Name = "Column11";
			this.Column11.ReadOnly = true;
			// 
			// Column12
			// 
			this.Column12.HeaderText = "Column12";
			this.Column12.Name = "Column12";
			this.Column12.ReadOnly = true;
			// 
			// MainList_MenuStrip
			// 
			this.MainList_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoColumnsSize_MenuItem,
            this.AutoColumnsSizeAndHeader_MenuItem,
            this.toolStripSeparator7,
            this.DisplayedColumns_MenuItem,
            this.ShowLogForm_MenuItem});
			this.MainList_MenuStrip.Name = "MainList_MenuStrip";
			this.MainList_MenuStrip.Size = new System.Drawing.Size(233, 98);
			// 
			// DateList
			// 
			this.DateList.AllowDrop = true;
			this.DateList.AllowUserToAddRows = false;
			this.DateList.AllowUserToDeleteRows = false;
			this.DateList.AllowUserToOrderColumns = true;
			this.DateList.AllowUserToResizeRows = false;
			this.DateList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DateList.BackgroundColor = System.Drawing.SystemColors.Window;
			this.DateList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DateList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DateList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.DateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DateList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.textAndImageColumn1});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DateList.DefaultCellStyle = dataGridViewCellStyle2;
			this.DateList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.DateList.GridColor = System.Drawing.SystemColors.Control;
			this.DateList.Location = new System.Drawing.Point(0, 531);
			this.DateList.MultiSelect = false;
			this.DateList.Name = "DateList";
			this.DateList.ReadOnly = true;
			this.DateList.RowHeadersVisible = false;
			this.DateList.Size = new System.Drawing.Size(912, 125);
			this.DateList.TabIndex = 8;
			this.DateList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DateList_CellFormatting);
			this.DateList.Enter += new System.EventHandler(this.DateList_Enter);
			this.DateList.Leave += new System.EventHandler(this.DateList_Leave);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
			this.dataGridViewTextBoxColumn1.Image = null;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// textAndImageColumn1
			// 
			this.textAndImageColumn1.HeaderText = "Column2";
			this.textAndImageColumn1.Name = "textAndImageColumn1";
			this.textAndImageColumn1.ReadOnly = true;
			this.textAndImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// AutoColumnsSize_MenuItem
			// 
			this.AutoColumnsSize_MenuItem.Name = "AutoColumnsSize_MenuItem";
			this.AutoColumnsSize_MenuItem.Size = new System.Drawing.Size(232, 22);
			this.AutoColumnsSize_MenuItem.Text = "自动调整列宽";
			this.AutoColumnsSize_MenuItem.Click += new System.EventHandler(this.AutoColumnSize_MenuItem_Click);
			// 
			// AutoColumnsSizeAndHeader_MenuItem
			// 
			this.AutoColumnsSizeAndHeader_MenuItem.Name = "AutoColumnsSizeAndHeader_MenuItem";
			this.AutoColumnsSizeAndHeader_MenuItem.Size = new System.Drawing.Size(232, 22);
			this.AutoColumnsSizeAndHeader_MenuItem.Text = "自动调整列宽（包括列标题）";
			this.AutoColumnsSizeAndHeader_MenuItem.Click += new System.EventHandler(this.AutoColumnsSizeAndHeader_MenuItem_Click);
			// 
			// DisplayedColumns_MenuItem
			// 
			this.DisplayedColumns_MenuItem.Name = "DisplayedColumns_MenuItem";
			this.DisplayedColumns_MenuItem.Size = new System.Drawing.Size(232, 22);
			this.DisplayedColumns_MenuItem.Text = "选择显示的列";
			this.DisplayedColumns_MenuItem.Click += new System.EventHandler(this.DisplayedColumns_MenuItem_Click);
			// 
			// ShowLogForm_MenuItem
			// 
			this.ShowLogForm_MenuItem.Name = "ShowLogForm_MenuItem";
			this.ShowLogForm_MenuItem.Size = new System.Drawing.Size(232, 22);
			this.ShowLogForm_MenuItem.Text = "属性";
			this.ShowLogForm_MenuItem.Click += new System.EventHandler(this.ShowLogForm_MenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(229, 6);
			// 
			// Reset_MenuItem
			// 
			this.Reset_MenuItem.Name = "Reset_MenuItem";
			this.Reset_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.Reset_MenuItem.Text = "计数重置";
			this.Reset_MenuItem.Click += new System.EventHandler(this.Reset_MenuItem_Click);
			// 
			// LoadFile_MenuItem
			// 
			this.LoadFile_MenuItem.Name = "LoadFile_MenuItem";
			this.LoadFile_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.LoadFile_MenuItem.Text = "从文件载入";
			this.LoadFile_MenuItem.Click += new System.EventHandler(this.LoadFile_MenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(912, 678);
			this.Controls.Add(this.MainList);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.DateList);
			this.Controls.Add(this.statusStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "TCPingInfoView";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.NotifyIcon_MenuStrip.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainList)).EndInit();
			this.MainList_MenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DateList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MenuStrip menuStrip1;
		private ToolStripMenuItem File_MenuItem;
		private StatusStrip statusStrip1;
		private NotifyIcon notifyIcon1;
		private ContextMenuStrip NotifyIcon_MenuStrip;
		private ToolStripMenuItem ShowHide_MenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem StartStop_MenuItem;
		private ToolStripMenuItem Exit_MenuItem;
		private ToolStripStatusLabel toolStripStatusLabel1;
		private ColumnHeader Date;
		private ColumnHeader Latency2;
		private DoubleBufferDataGridView MainList;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private ImageList imageList1;
		private Splitter splitter1;
		private DoubleBufferDataGridView DateList;
		private TextAndImageColumn dataGridViewTextBoxColumn1;
		private DataGridViewTextBoxColumn textAndImageColumn1;
		private ToolStripButton Load_Button;
		private ToolStripButton Test_Button;
		private ToolStripButton Start_Button;
		private ToolStripButton Exit_Button;
		private ToolStrip toolStrip1;
		private ToolStripMenuItem Options_MenuItem;
		private ToolStripMenuItem IsNotifyClose_MenuItem;
		private ToolStripMenuItem IsShowDateList_MenuItem;
		private ToolStripMenuItem View_MenuItem;
		private DataGridViewTextBoxColumn Column1;
		private TextAndImageColumn Column2;
		private DataGridViewTextBoxColumn Column3;
		private DataGridViewTextBoxColumn Column4;
		private DataGridViewTextBoxColumn Column5;
		private DataGridViewTextBoxColumn Column6;
		private DataGridViewTextBoxColumn Column7;
		private DataGridViewTextBoxColumn Column8;
		private DataGridViewTextBoxColumn Column9;
		private DataGridViewTextBoxColumn Column10;
		private DataGridViewTextBoxColumn Column11;
		private DataGridViewTextBoxColumn Column12;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem Help_MenuItem;
		private ToolStripMenuItem About_MenuItem;
		private ContextMenuStrip MainList_MenuStrip;
		private ToolStripMenuItem AutoColumnsSize_MenuItem;
		private ToolStripMenuItem AutoColumnsSizeAndHeader_MenuItem;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripMenuItem DisplayedColumns_MenuItem;
		private ToolStripMenuItem ShowLogForm_MenuItem;
		private ToolStripMenuItem LoadFile_MenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem Reset_MenuItem;
	}
}

