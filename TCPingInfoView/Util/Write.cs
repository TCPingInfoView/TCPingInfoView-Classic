using System;
using System.IO;
using System.Text;

namespace TCPingInfoView.Util
{
	public static class Write
	{
		private static readonly UTF8Encoding Utf8WithoutBom = new UTF8Encoding(false);
		
		public static void WriteToFile(string path,string str)
		{
			try
			{
				using (var fileS = new FileStream(path, FileMode.Create, FileAccess.Write))
				{
					using (var sw = new StreamWriter(fileS, Utf8WithoutBom))
					{
						sw.Write(str);
					}
				}
			}
			catch (Exception)
			{
				// ignored
			}
		}
	}
}
