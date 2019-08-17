using System;
using TCPingInfoViewLib.Model;

namespace TCPingInfoView.ViewModel
{
	public class TestResult : ViewModelBase
	{
		private TCPingStatus _tcpingResult;
		private ICMPingStatus _pingResult;
		private DateTime _time;

		public TestResult()
		{
			_time = DateTime.MinValue;
			_tcpingResult = null;
			_pingResult = null;
		}

		public TCPingStatus TCPingResult
		{
			get => _tcpingResult;
			set
			{
				if (_tcpingResult != value)
				{
					_tcpingResult = value;
					OnPropertyChanged();
				}
			}
		}

		public ICMPingStatus PingResult
		{
			get => _pingResult;
			set
			{
				if (_pingResult != value)
				{
					_pingResult = value;
					OnPropertyChanged();
				}
			}
		}

		public DateTime Time
		{
			get => _time;
			set
			{
				if (_time != value)
				{
					_time = value;
					OnPropertyChanged();
				}
			}
		}
	}
}