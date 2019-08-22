using System.Collections.Generic;

namespace TCPingInfoViewLib.Comparer
{
	public class VersionComparer : IComparer<object>
	{
		public int Compare(object x, object y)
		{
			return Utils.VersionUtil.CompareVersion(x?.ToString(), y?.ToString());
		}
	}
}
