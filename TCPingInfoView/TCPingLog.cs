using System;
using System.Collections.Generic;

namespace TCPingInfoView
{
	public class TCPingInfo
	{
		public DateTime Date;
		public double Latenty;
	}

	public class TCPingLog
	{
		public readonly List<TCPingInfo> Info;
		private double TotalPing = 0;
		public int Count = 0;
		public int SucceedCount = 0;
		public int FailedCount = 0;
		public double LastPing = 0;
		public double MaxPing = 0;
		public double MinPing = double.MaxValue;

		public double Average => TotalPing / SucceedCount;
		public double FailedP => (double)FailedCount / Count;
		public double SucceedP => (double) SucceedCount / Count;

		public TCPingLog()
		{
			Info = new List<TCPingInfo>();
		}

		public void Add(TCPingInfo info)
		{
			Info.Add(info);
			++Count;
			if (info.Latenty < MainForm.Timeout)
			{
				++SucceedCount;
				TotalPing += info.Latenty;
				LastPing = info.Latenty;

				if (MaxPing < LastPing)
				{
					MaxPing = LastPing;
				}

				if (MinPing > LastPing)
				{
					MinPing = LastPing;
				}
			}
			else
			{
				++FailedCount;
			}
		}
	}
}