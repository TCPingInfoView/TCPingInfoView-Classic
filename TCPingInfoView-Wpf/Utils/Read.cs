using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.Utils
{
	public static class Read
	{
		private static readonly UTF8Encoding Utf8WithoutBom = new UTF8Encoding(false);

		public static List<EndPointInfo> ReadEndPointFromString(string s)
		{
			var lines = s.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			var sl = lines.Where(line => !string.IsNullOrWhiteSpace(line) && line[0] != '#');

			return Util.ToEndPoints(sl);
		}

		public static string ReadTextFromFile(string path)
		{
			using (var sr = new StreamReader(path, Utf8WithoutBom))
			{
				return sr.ReadToEnd();
			}
		}

		public static string GetFilePath()
		{
			var path = string.Empty;
			var openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
				Title = @"请选择包含地址的文件",
				Filter = @"文本文件 (*.txt)|*.txt",
				InitialDirectory = Path.GetDirectoryName(Util.GetExecutablePath())
			};
			var result = openFileDialog.ShowDialog();
			if (result == true)
			{
				path = openFileDialog.FileName;
			}

			return path;
		}
	}
}
