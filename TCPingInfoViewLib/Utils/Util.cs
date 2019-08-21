using System;

namespace TCPingInfoViewLib.Utils
{
	public static class Util
	{
		/// <returns> =0:versions are equal</returns>
		/// <returns> &gt;0:version1 is greater</returns>
		/// <returns> &lt;0:version2 is greater</returns>
		public static int CompareVersion(string v1, string v2)
		{
			var version1 = new Version(v1);
			var version2 = new Version(v2);
			var res = version1.CompareTo(version2);
			Console.WriteLine($@"{v1} {v2}:	{res}");
			return res;
		}
	}
}
