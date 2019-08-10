using System.Net.NetworkInformation;

namespace TCPingInfoViewLib.Model
{
	public class TCPingStatus
	{
		public TCPingStatus()
		{
			Status = IPStatus.Unknown;
			RTT = 0;
		}

		public IPStatus Status;
		public long RTT;
	}
}
