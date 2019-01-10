namespace TCPingInfoView.Forms
{
	partial class TCPingOptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.PingInterval = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.HighPing = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.TimeoutPing = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DNSTimeoutPing = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.OK_button = new System.Windows.Forms.Button();
			this.Cancel_button = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.Timeout_button = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.HighPing_button = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.LowPing_button = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PingInterval)).BeginInit();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.HighPing)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TimeoutPing)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DNSTimeoutPing)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "节点列表：";
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.AcceptsTab = true;
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(15, 29);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(612, 220);
			this.textBox1.TabIndex = 2;
			this.textBox1.WordWrap = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(143, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "反向 DNS 解析延迟(ms)：";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.panel4);
			this.groupBox1.Controls.Add(this.panel3);
			this.groupBox1.Controls.Add(this.panel2);
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Location = new System.Drawing.Point(15, 255);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(612, 144);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "时间";
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.Controls.Add(this.PingInterval);
			this.panel4.Controls.Add(this.label5);
			this.panel4.Location = new System.Drawing.Point(6, 116);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(600, 26);
			this.panel4.TabIndex = 7;
			// 
			// PingInterval
			// 
			this.PingInterval.Dock = System.Windows.Forms.DockStyle.Right;
			this.PingInterval.Location = new System.Drawing.Point(480, 0);
			this.PingInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.PingInterval.Name = "PingInterval";
			this.PingInterval.Size = new System.Drawing.Size(120, 21);
			this.PingInterval.TabIndex = 6;
			this.PingInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Left;
			this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(107, 12);
			this.label5.TabIndex = 2;
			this.label5.Text = "TCPing 间隔(秒)：";
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.HighPing);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Location = new System.Drawing.Point(6, 84);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(600, 26);
			this.panel3.TabIndex = 7;
			// 
			// HighPing
			// 
			this.HighPing.Dock = System.Windows.Forms.DockStyle.Right;
			this.HighPing.Location = new System.Drawing.Point(480, 0);
			this.HighPing.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.HighPing.Name = "HighPing";
			this.HighPing.Size = new System.Drawing.Size(120, 21);
			this.HighPing.TabIndex = 5;
			this.HighPing.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Left;
			this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "高延迟(ms)：";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.TimeoutPing);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Location = new System.Drawing.Point(6, 52);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(600, 26);
			this.panel2.TabIndex = 7;
			// 
			// TimeoutPing
			// 
			this.TimeoutPing.Dock = System.Windows.Forms.DockStyle.Right;
			this.TimeoutPing.Location = new System.Drawing.Point(480, 0);
			this.TimeoutPing.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.TimeoutPing.Name = "TimeoutPing";
			this.TimeoutPing.Size = new System.Drawing.Size(120, 21);
			this.TimeoutPing.TabIndex = 4;
			this.TimeoutPing.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.TimeoutPing.ValueChanged += new System.EventHandler(this.HighPing_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "超时延迟(ms)：";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.DNSTimeoutPing);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Location = new System.Drawing.Point(6, 20);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(600, 26);
			this.panel1.TabIndex = 6;
			// 
			// DNSTimeoutPing
			// 
			this.DNSTimeoutPing.Dock = System.Windows.Forms.DockStyle.Right;
			this.DNSTimeoutPing.Location = new System.Drawing.Point(480, 0);
			this.DNSTimeoutPing.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.DNSTimeoutPing.Name = "DNSTimeoutPing";
			this.DNSTimeoutPing.Size = new System.Drawing.Size(120, 21);
			this.DNSTimeoutPing.TabIndex = 3;
			this.DNSTimeoutPing.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Location = new System.Drawing.Point(15, 457);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(612, 54);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "自定义标题";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(15, 21);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(585, 21);
			this.textBox2.TabIndex = 7;
			// 
			// OK_button
			// 
			this.OK_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OK_button.Location = new System.Drawing.Point(471, 517);
			this.OK_button.Name = "OK_button";
			this.OK_button.Size = new System.Drawing.Size(75, 23);
			this.OK_button.TabIndex = 0;
			this.OK_button.Text = "确定";
			this.OK_button.UseVisualStyleBackColor = true;
			this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
			// 
			// Cancel_button
			// 
			this.Cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_button.Location = new System.Drawing.Point(552, 517);
			this.Cancel_button.Name = "Cancel_button";
			this.Cancel_button.Size = new System.Drawing.Size(75, 23);
			this.Cancel_button.TabIndex = 1;
			this.Cancel_button.Text = "取消";
			this.Cancel_button.UseVisualStyleBackColor = true;
			this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox4.Controls.Add(this.Timeout_button);
			this.groupBox4.Location = new System.Drawing.Point(15, 405);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 46);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "超时颜色";
			// 
			// Timeout_button
			// 
			this.Timeout_button.BackColor = System.Drawing.Color.Red;
			this.Timeout_button.Location = new System.Drawing.Point(9, 17);
			this.Timeout_button.Name = "Timeout_button";
			this.Timeout_button.Size = new System.Drawing.Size(48, 23);
			this.Timeout_button.TabIndex = 2;
			this.Timeout_button.TabStop = false;
			this.Timeout_button.UseVisualStyleBackColor = false;
			this.Timeout_button.Click += new System.EventHandler(this.ColorButton_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox3.Controls.Add(this.HighPing_button);
			this.groupBox3.Location = new System.Drawing.Point(221, 405);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 46);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "高延迟颜色";
			// 
			// HighPing_button
			// 
			this.HighPing_button.BackColor = System.Drawing.Color.Coral;
			this.HighPing_button.Location = new System.Drawing.Point(9, 17);
			this.HighPing_button.Name = "HighPing_button";
			this.HighPing_button.Size = new System.Drawing.Size(48, 23);
			this.HighPing_button.TabIndex = 3;
			this.HighPing_button.TabStop = false;
			this.HighPing_button.UseVisualStyleBackColor = false;
			this.HighPing_button.Click += new System.EventHandler(this.ColorButton_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox5.Controls.Add(this.LowPing_button);
			this.groupBox5.Location = new System.Drawing.Point(427, 405);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(200, 46);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "低延迟颜色";
			// 
			// LowPing_button
			// 
			this.LowPing_button.BackColor = System.Drawing.Color.Green;
			this.LowPing_button.Location = new System.Drawing.Point(9, 17);
			this.LowPing_button.Name = "LowPing_button";
			this.LowPing_button.Size = new System.Drawing.Size(48, 23);
			this.LowPing_button.TabIndex = 4;
			this.LowPing_button.TabStop = false;
			this.LowPing_button.UseVisualStyleBackColor = false;
			this.LowPing_button.Click += new System.EventHandler(this.ColorButton_Click);
			// 
			// TCPingOptionsForm
			// 
			this.AcceptButton = this.OK_button;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel_button;
			this.ClientSize = new System.Drawing.Size(636, 552);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.Cancel_button);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.OK_button);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "TCPingOptionsForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TCPing 选项";
			this.Load += new System.EventHandler(this.TCPingOptionsForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PingInterval)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.HighPing)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TimeoutPing)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DNSTimeoutPing)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown DNSTimeoutPing;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.NumericUpDown PingInterval;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.NumericUpDown HighPing;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.NumericUpDown TimeoutPing;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button OK_button;
		private System.Windows.Forms.Button Cancel_button;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button Timeout_button;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button HighPing_button;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button LowPing_button;
	}
}