using System.Collections.Generic;

namespace TCPingInfoView
{
	public class TCPingLog
	{
		private readonly ConcurrentList<DateTable> _info;
		private double TotalPing = 0;
		public int Count = 0;
		public int SucceedCount = 0;
		public int FailedCount = 0;
		public double LastPing = 0;
		public double MaxPing = 0;
		public double MinPing = double.MaxValue;

		public double Average => TotalPing / SucceedCount;
		public double FailedP => (double)FailedCount / Count;
		public double SucceedP => (double)SucceedCount / Count;

		public IEnumerable<DateTable> Info => _info;

		public TCPingLog()
		{
			_info = new ConcurrentList<DateTable>();
		}

		public void Add(DateTable info)
		{
			_info.Add(info);
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