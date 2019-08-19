using System.IO;
using System.Text.Json;
using TCPingInfoView.Model;

namespace TCPingInfoView.Utils
{
	public static class Write
	{
		public static void WriteToFile(string path, string str)
		{
			using (var fileS = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (var sw = new StreamWriter(fileS, Util.Utf8WithoutBom))
				{
					sw.Write(str);
				}
			}
		}

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
	}
}
