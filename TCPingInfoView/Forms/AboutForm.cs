using System;
using System.Reflection;
using System.Windows.Forms;

namespace TCPingInfoView.Forms
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
		}

		private static Assembly Asm => Assembly.GetExecutingAssembly();
		private static string ExeName => Asm.GetName().Name;
		private static string Version => Application.ProductVersion;
		private static AssemblyCopyrightAttribute CopyrightAttribute => (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Asm, typeof(AssemblyCopyrightAttribute));

		private void AboutForm_Load(object sender, EventArgs e)
		{
			Text = ExeName;
			label1.Text = $@"{ExeName} v{Version}";
			label2.Text = $@"{CopyrightAttribute.Copyright} HMBSbige";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(linkLabel1.Text);
		}
	}
}
