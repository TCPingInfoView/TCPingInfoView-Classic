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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.File_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.从文件载入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.Start_Button = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.NotifyIcon_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ShowHide_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView2 = new TCPingInfoView.GridLineDataGridView();
			this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Latency2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridView1 = new TCPingInfoView.GridLineDataGridView();
			this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Hostname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IPPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FailedP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Latency1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Exit_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StartStop_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.NotifyIcon_MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
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
			this.NotifyIcon_MenuStrip.Size = new System.Drawing.Size(181, 98);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
			// 
			// ShowHide_MenuItem
			// 
			this.ShowHide_MenuItem.Name = "ShowHide_MenuItem";
			this.ShowHide_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.ShowHide_MenuItem.Text = "显示/隐藏";
			this.ShowHide_MenuItem.Click += new System.EventHandler(this.ShowHide_MenuItem_Click);
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToDeleteRows = false;
			this.dataGridView2.AllowUserToResizeRows = false;
			this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle31.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Latency2});
			dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle32.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle32;
			this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView2.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridView2.Location = new System.Drawing.Point(0, 545);
			this.dataGridView2.MultiSelect = false;
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.ReadOnly = true;
			this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle33.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
			this.dataGridView2.RowHeadersVisible = false;
			this.dataGridView2.RowTemplate.Height = 23;
			this.dataGridView2.RowTemplate.ReadOnly = true;
			this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView2.Size = new System.Drawing.Size(912, 111);
			this.dataGridView2.TabIndex = 2;
			// 
			// Date
			// 
			this.Date.HeaderText = "TCP 连接时间";
			this.Date.Name = "Date";
			this.Date.ReadOnly = true;
			// 
			// Latency2
			// 
			this.Latency2.HeaderText = "延迟";
			this.Latency2.Name = "Latency2";
			this.Latency2.ReadOnly = true;
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
			dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle34.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Hostname,
            this.IPPort,
            this.FailedP,
            this.Latency1,
            this.Description});
			dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle35.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle35;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridView1.Location = new System.Drawing.Point(0, 50);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle36.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.RowTemplate.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(912, 495);
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
			// Exit_MenuItem
			// 
			this.Exit_MenuItem.Name = "Exit_MenuItem";
			this.Exit_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.Exit_MenuItem.Text = "退出";
			this.Exit_MenuItem.Click += new System.EventHandler(this.Exit_MenuItem_Click);
			// 
			// StartStop_MenuItem
			// 
			this.StartStop_MenuItem.Name = "StartStop_MenuItem";
			this.StartStop_MenuItem.Size = new System.Drawing.Size(180, 22);
			this.StartStop_MenuItem.Text = "开始";
			this.StartStop_MenuItem.Click += new System.EventHandler(this.StartStop_MenuItem_Click);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(277, 17);
			this.toolStripStatusLabel1.Text = "https://github.com/HMBSbige/TCPingInfoView";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(912, 678);
			this.Controls.Add(this.dataGridView2);
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
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
		private GridLineDataGridView dataGridView2;
		private StatusStrip statusStrip1;
		private ToolStrip toolStrip1;
		private ToolStripButton toolStripButton1;
		private ToolStripButton toolStripButton2;
		private ToolStripButton toolStripButton3;
		private DataGridViewTextBoxColumn Date;
		private DataGridViewTextBoxColumn Latency2;
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
	}
}

