using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.File_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.从文件载入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.Start_Button = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.NotifyIcon_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ShowHide_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StartStop_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.Exit_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DatelistView = new System.Windows.Forms.ListView();
			this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Latency2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.dataGridView1 = new TCPingInfoView.GridLineDataGridView();
			this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Hostname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IPPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FailedP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Latency1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.NotifyIcon_MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_MenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(912, 25);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// File_MenuItem
			// 
			this.File_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.从文件载入ToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
			this.File_MenuItem.Name = "File_MenuItem";
			this.File_MenuItem.Size = new System.Drawing.Size(44, 21);
			this.File_MenuItem.Text = "文件";
			// 
			// 从文件载入ToolStripMenuItem
			// 
			this.从文件载入ToolStripMenuItem.Name = "从文件载入ToolStripMenuItem";
			this.从文件载入ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.从文件载入ToolStripMenuItem.Text = "从文件载入";
			this.从文件载入ToolStripMenuItem.Click += new System.EventHandler(this.从文件载入ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
			// 
			// 退出ToolStripMenuItem
			// 
			this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
			this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.退出ToolStripMenuItem.Text = "退出";
			this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
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
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton1,
            this.Start_Button,
            this.toolStripButton2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 25);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(912, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(36, 22);
			this.toolStripButton3.Text = "载入";
			this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(36, 22);
			this.toolStripButton1.Text = "测试";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// Start_Button
			// 
			this.Start_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.Start_Button.Image = ((System.Drawing.Image)(resources.GetObject("Start_Button.Image")));
			this.Start_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Start_Button.Name = "Start_Button";
			this.Start_Button.Size = new System.Drawing.Size(36, 22);
			this.Start_Button.Text = "开始";
			this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(36, 22);
			this.toolStripButton2.Text = "退出";
			this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
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
            this.StartStop_MenuItem,
            this.toolStripSeparator2,
            this.Exit_MenuItem});
			this.NotifyIcon_MenuStrip.Name = "NotifyIcon_MenuStrip";
			this.NotifyIcon_MenuStrip.Size = new System.Drawing.Size(130, 76);
			// 
			// ShowHide_MenuItem
			// 
			this.ShowHide_MenuItem.Name = "ShowHide_MenuItem";
			this.ShowHide_MenuItem.Size = new System.Drawing.Size(129, 22);
			this.ShowHide_MenuItem.Text = "显示/隐藏";
			this.ShowHide_MenuItem.Click += new System.EventHandler(this.ShowHide_MenuItem_Click);
			// 
			// StartStop_MenuItem
			// 
			this.StartStop_MenuItem.Name = "StartStop_MenuItem";
			this.StartStop_MenuItem.Size = new System.Drawing.Size(129, 22);
			this.StartStop_MenuItem.Text = "开始";
			this.StartStop_MenuItem.Click += new System.EventHandler(this.StartStop_MenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(126, 6);
			// 
			// Exit_MenuItem
			// 
			this.Exit_MenuItem.Name = "Exit_MenuItem";
			this.Exit_MenuItem.Size = new System.Drawing.Size(129, 22);
			this.Exit_MenuItem.Text = "退出";
			this.Exit_MenuItem.Click += new System.EventHandler(this.Exit_MenuItem_Click);
			// 
			// DatelistView
			// 
			this.DatelistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Date,
            this.Latency2});
			this.DatelistView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DatelistView.FullRowSelect = true;
			this.DatelistView.GridLines = true;
			this.DatelistView.Location = new System.Drawing.Point(0, 551);
			this.DatelistView.MultiSelect = false;
			this.DatelistView.Name = "DatelistView";
			this.DatelistView.ShowItemToolTips = true;
			this.DatelistView.Size = new System.Drawing.Size(912, 105);
			this.DatelistView.TabIndex = 5;
			this.DatelistView.UseCompatibleStateImageBehavior = false;
			this.DatelistView.View = System.Windows.Forms.View.Details;
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
			// dataGridView1
			// 
			this.dataGridView1.AllowDrop = true;
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Hostname,
            this.IPPort,
            this.FailedP,
            this.Latency1,
            this.Description});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridView1.Location = new System.Drawing.Point(0, 50);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.RowTemplate.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(912, 501);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
			this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
			this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
			// 
			// Index
			// 
			this.Index.HeaderText = "列表顺序";
			this.Index.Name = "Index";
			this.Index.ReadOnly = true;
			// 
			// Hostname
			// 
			this.Hostname.HeaderText = "主机名";
			this.Hostname.Name = "Hostname";
			this.Hostname.ReadOnly = true;
			// 
			// IPPort
			// 
			this.IPPort.HeaderText = "IP:端口";
			this.IPPort.Name = "IPPort";
			this.IPPort.ReadOnly = true;
			// 
			// FailedP
			// 
			this.FailedP.HeaderText = "失败率";
			this.FailedP.Name = "FailedP";
			this.FailedP.ReadOnly = true;
			// 
			// Latency1
			// 
			this.Latency1.HeaderText = "延迟(ms)";
			this.Latency1.Name = "Latency1";
			this.Latency1.ReadOnly = true;
			// 
			// Description
			// 
			this.Description.HeaderText = "说明";
			this.Description.Name = "Description";
			this.Description.ReadOnly = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(912, 678);
			this.Controls.Add(this.DatelistView);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
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
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.NotifyIcon_MenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MenuStrip menuStrip1;
		private ToolStripMenuItem File_MenuItem;
		private ToolStripMenuItem 从文件载入ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem 退出ToolStripMenuItem;
		private GridLineDataGridView dataGridView1;
		private StatusStrip statusStrip1;
		private ToolStrip toolStrip1;
		private ToolStripButton toolStripButton1;
		private ToolStripButton toolStripButton2;
		private ToolStripButton toolStripButton3;
		private DataGridViewTextBoxColumn Index;
		private DataGridViewTextBoxColumn Hostname;
		private DataGridViewTextBoxColumn IPPort;
		private DataGridViewTextBoxColumn FailedP;
		private DataGridViewTextBoxColumn Latency1;
		private DataGridViewTextBoxColumn Description;
		private ToolStripButton Start_Button;
		private NotifyIcon notifyIcon1;
		private ContextMenuStrip NotifyIcon_MenuStrip;
		private ToolStripMenuItem ShowHide_MenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem StartStop_MenuItem;
		private ToolStripMenuItem Exit_MenuItem;
		private ToolStripStatusLabel toolStripStatusLabel1;
		private ListView DatelistView;
		private ColumnHeader Date;
		private ColumnHeader Latency2;
	}
}

