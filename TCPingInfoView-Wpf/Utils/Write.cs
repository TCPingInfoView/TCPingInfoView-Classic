using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TCPingInfoView.Model;

namespace TCPingInfoView.Utils
{
	public static class Write
	{
		public static void SaveConfig(Config cfg)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			var jsonStr = JsonSerializer.Serialize(cfg, options);

			var fileLocation = Path.Combine(Util.CurrentDirectory, Util.ConfigFileName);

			File.WriteAllTextAsync(fileLocation, jsonStr, Util.Utf8WithoutBom);
		}

		public static void SaveConfig(IEnumerable<EndPointInfo> cfg)
		{
			try
			{
				var path = GetFilePath();
				if (string.IsNullOrEmpty(path))
				{
					return;
				}
				path = Path.GetFullPath(path);

				var options = new JsonSerializerOptions
				{
					WriteIndented = true
				};

				var jsonStr = JsonSerializer.Serialize(cfg, options);

				File.WriteAllTextAsync(path, jsonStr, Util.Utf8WithoutBom);
			}
			catch (Exception ex)
			{
				Util.ShowExceptionMessageBox(ex);
			}
		}

		private static string GetFilePath()
		{
			var path = string.Empty;
			var saveFileDialog = new SaveFileDialog
			{
				Filter = I18NUtil.GetAppStringValue(@"SelectJsonFileFilter")
			};
			var result = saveFileDialog.ShowDialog();
			if (result == true)
			{
				path = saveFileDialog.FileName;
			}
			return path;
		}
	}
}
