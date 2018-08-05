using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TCPingInfoView
{
	public static class Read
	{
		public static IEnumerable<string> ReadAddress(string path)
		{
			var sl = new List<string>();
			using (var sr = new StreamReader(path, Encoding.UTF8))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					sl.Add(line);
				}
			}
			return sl;
		}

		public static string GetFilePath()
		{
			var path = string.Empty;
			var openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
				Title = @"请选择包含地址的文件",
				Filter = @"文本文件 (*.txt)|*.txt",
				InitialDirectory = Application.ExecutablePath
			};
			var result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				path = openFileDialog.FileName;
			}
			return path;
		}
	}
}
