using System;
using System.Collections.Generic;
using System.Windows.Media;
using TCPingInfoView.Utils;
using TCPingInfoViewLib.NetUtils;

namespace TCPingInfoView.Model
{
	[Serializable]
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

		public string Language { get; set; }

		public bool AllowPreRelease { get; set; }

		public ColumnsStatus ColumnsStatus { get; set; }

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
			Language = I18NUtil.GetLanguage();
			AllowPreRelease = false;
			ColumnsStatus = new ColumnsStatus();
			EndPointInfo = new EndPointInfo[0];
		}

		public void Fix()
		{
			Language = I18NUtil.GetLanguage(Language);

			if (Interval < 1)
			{
				Interval = 1;
			}
			else if (int.MaxValue / 1000.0 < Interval)
			{
				Interval = int.MaxValue / 1000;
			}

			if (TCPingTimeout > Math.Min(Interval * 1000, NetTest.TCPMaxTimeout))
			{
				TCPingTimeout = Math.Min(Interval * 1000, NetTest.TCPMaxTimeout);
			}
			else if (TCPingTimeout < 1)
			{
				TCPingTimeout = 1;
			}

			if (LongTCPingTimeout > TCPingTimeout)
			{
				LongTCPingTimeout = TCPingTimeout;
			}
			else if (LongTCPingTimeout < 1)
			{
				TCPingTimeout = 1;
			}

			if (PingTimeout > Interval * 1000)
			{
				PingTimeout = Interval * 1000;
			}
			else if (PingTimeout < 1)
			{
				PingTimeout = 1;
			}

			if (LongPingTimeout > PingTimeout)
			{
				LongPingTimeout = PingTimeout;
			}
			else if (LongPingTimeout < 1)
			{
				LongPingTimeout = 1;
			}

			if (DNSTimeout > Interval * 1000)
			{
				DNSTimeout = Interval * 1000;
			}
			else if (DNSTimeout < 1)
			{
				DNSTimeout = 1;
			}

			if (ReverseDNSTimeout > Interval * 1000)
			{
				ReverseDNSTimeout = Interval * 1000;
			}
			else if (ReverseDNSTimeout < 1)
			{
				ReverseDNSTimeout = 1;
			}
		}
	}
}
