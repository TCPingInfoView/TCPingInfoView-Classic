using System.Collections.Generic;
using System.Windows.Media;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.Model
{
	public class Config
	{
		public double StartTop { get; set; }
		public double StartLeft { get; set; }
		public double StartWidth { get; set; }
		public double StartHeight { get; set; }
		public bool Topmost { get; set; }
		public int Interval { get; set; }

		public Color FailedBackgroundColor { get; set; }
		public Color SuccessForegroundColor { get; set; }
		public Color LongPingForegroundColor { get; set; }

		public int LongPingTimeout { get; set; }
		public int PingTimeout { get; set; }
		public int LongTCPingTimeout { get; set; }
		public int TCPingTimeout { get; set; }
		public int DNSTimeout { get; set; }
		public int ReverseDNSTimeout { get; set; }

		public IEnumerable<EndPointInfo> EndPointInfo { get; set; }

		public Config()
		{
			StartTop = 0;
			StartLeft = 0;
			StartWidth = 1000;
			StartHeight = 618;
			Topmost = false;
			Interval = 60;
			FailedBackgroundColor = Colors.Red;
			SuccessForegroundColor = Colors.Green;
			LongPingForegroundColor = Colors.Coral;
			LongPingTimeout = 300;
			PingTimeout = 3000;
			LongTCPingTimeout = 300;
			TCPingTimeout = 3000;
			DNSTimeout = 3000;
			ReverseDNSTimeout = 3000;
			EndPointInfo = new List<EndPointInfo>();
		}
	}
}
