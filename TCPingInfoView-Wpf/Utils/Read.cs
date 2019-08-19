using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TCPingInfoView.Model;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.Utils
{
	public static class Read
	{
		public static IEnumerable<EndPointInfo> ReadEndPointFromString(string s)
		{
			var lines = s.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			var sl = lines.Where(line => !string.IsNullOrWhiteSpace(line) && line[0] != '#');

			return Util.ToEndPoints(sl);
		}

		public static string ReadTextFromFile(string path)
		{
			return File.ReadAllText(path, Util.Utf8WithoutBom);
		}

		public static string GetFilePath()
		{
			var path = string.Empty;
			var openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
				Title = I18NUtil.GetAppStringValue(@"SelectAddressFileTitle"),
				Filter = I18NUtil.GetAppStringValue(@"SelectAddressFileFilter")
			};
			var result = openFileDialog.ShowDialog();
			if (result == true)
			{
				path = openFileDialog.FileName;
			}

			return path;
		}

		public static Config LoadConfig()
		{
			var fileLocation = Path.Combine(Util.CurrentDirectory, Util.ConfigFileName);
			if (File.Exists(fileLocation))
			{
				var jsonStr = ReadTextFromFile(fileLocation);
				try
				{
					return JsonSerializer.Deserialize<Config>(jsonStr);
				}
				catch
				{
					// ignored
				}
			}
			return null;
		}
	}
}
