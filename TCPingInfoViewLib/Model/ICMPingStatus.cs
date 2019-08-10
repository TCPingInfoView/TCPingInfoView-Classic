using System.Net;
using System.Net.NetworkInformation;

namespace TCPingInfoViewLib.Model
{
	public class ICMPingStatus
	{
		public ICMPingStatus()
		{
			Status = IPStatus.Unknown;
			Address = null;
			RTT = 0;
			TTL = null;
			bytes = 0;
		}

		public IPStatus Status;
		public IPAddress Address;
		public long RTT;
		public int? TTL;
		public int bytes;
	}
}
