using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TCPingInfoView.Model;

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

		public static string GetTxtFilePath()
		{
			var path = string.Empty;
			var openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
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
					var config = JsonSerializer.Deserialize<Config>(jsonStr);
					config.Fix();
					return config;
				}
				catch
				{
					// ignored
				}
			}
			return null;
		}

		public static IEnumerable<EndPointInfo> LoadEndPoint()
		{
			try
			{
				var openFileDialog = new OpenFileDialog
				{
					Multiselect = false,
					Filter = I18NUtil.GetAppStringValue(@"SelectJsonFileFilter")
				};
				var result = openFileDialog.ShowDialog();
				if (result == true)
				{
					var path = openFileDialog.FileName;
					if (File.Exists(path))
					{
						var jsonStr = ReadTextFromFile(path);
						var config = JsonSerializer.Deserialize<IEnumerable<EndPointInfo>>(jsonStr);
						return config;
					}
				}
			}
			catch (Exception ex)
			{
				Util.ShowExceptionMessageBox(ex);
			}
			return null;
		}

	}
}
