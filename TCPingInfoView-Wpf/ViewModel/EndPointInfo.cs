using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;

namespace TCPingInfoView.ViewModel
{
	public class EndPointInfo : ViewModelBase
	{
		public EndPointInfo(int i)
		{
			Index = i;
			_hostname = null;
			_ip = null;
			_port = 443;
			_description = string.Empty;
			_testResults = new ObservableCollection<TestResult>();
			Reset();
		}

		public void Reset()
		{
			TestResults.Clear();
			SucceedPingCount = 0;
			FailedPingCount = 0;
			_totalPing = 0;
			LastPing = null;
			MaxPing = null;
			MinPing = null;
		}

		private string _hostname;
		private IPAddress _ip;
		private ushort _port;
		private string _description;
		private ObservableCollection<TestResult> _testResults;

		private long _totalPing;
		private long _succeedPingCount;
		private long _failedPingCount;
		private long? _lastPing;
		private long? _maxPing;
		private long? _minPing;

		private long _totalTCPing;
		private long _succeedTCPingCount;
		private long _failedTCPingCount;
		private long? _lastTCPing;
		private long? _maxTCPing;
		private long? _minTCPing;

		public int Index { get; }

		public string Hostname
		{
			get => _hostname;
			set
			{
				if (_hostname != value)
				{
					_hostname = value;
					OnPropertyChanged();
				}
			}
		}

		public IPAddress Ip
		{
			get => _ip;
			set
			{
				if (_ip == null || !_ip.Equals(value))
				{
					_ip = value;
					OnPropertyChanged();
				}
			}
		}

		public ushort Port
		{
			get => _port;
			set
			{
				if (_port != value)
				{
					_port = value;
					OnPropertyChanged();
				}
			}
		}

		public string Description
		{
			get => _description;
			set
			{
				if (_description != value)
				{
					_description = value;
					OnPropertyChanged();
				}
			}
		}

		public ObservableCollection<TestResult> TestResults
		{
			get => _testResults;
			set
			{
				if (_testResults != value)
				{
					_testResults = value;
					OnPropertyChanged();
				}
			}
		}

		#region ICMPing

		public long SucceedPingCount
		{
			get => _succeedPingCount;
			private set
			{
				if (_succeedPingCount != value)
				{
					_succeedPingCount = value;
					OnPropertyChanged();
				}
			}
		}

		public long FailedPingCount
		{
			get => _failedPingCount;
			private set
			{
				if (_failedPingCount != value)
				{
					_failedPingCount = value;
					OnPropertyChanged();
				}
			}
		}

		public long? LastPing
		{
			get => _lastPing;
			private set
			{
				if (_lastPing != value)
				{
					_lastPing = value;
					OnPropertyChanged();
				}
			}
		}

		public long? MaxPing
		{
			get => _maxPing;
			private set
			{
				if (_maxPing != value)
				{
					_maxPing = value;
					OnPropertyChanged();
				}
			}
		}

		public long? MinPing
		{
			get => _minPing;
			private set
			{
				if (_minPing != value)
				{
					_minPing = value;
					OnPropertyChanged();
				}
			}
		}

		public long? AveragePing
		{
			get
			{
				if (SucceedPingCount != 0)
				{
					return Convert.ToInt64(_totalPing / SucceedPingCount);
				}
				return null;
			}
		}

		#endregion

		#region TCPing

		public long SucceedTCPingCount
		{
			get => _succeedTCPingCount;
			private set
			{
				if (_succeedTCPingCount != value)
				{
					_succeedTCPingCount = value;
					OnPropertyChanged();
				}
			}
		}

		public long FailedTCPingCount
		{
			get => _failedTCPingCount;
			private set
			{
				if (_failedTCPingCount != value)
				{
					_failedTCPingCount = value;
					OnPropertyChanged();
				}
			}
		}

		public long? LastTCPing
		{
			get => _lastTCPing;
			private set
			{
				if (_lastTCPing != value)
				{
					_lastTCPing = value;
					OnPropertyChanged();
				}
			}
		}

		public long? MaxTCPing
		{
			get => _maxTCPing;
			private set
			{
				if (_maxTCPing != value)
				{
					_maxTCPing = value;
					OnPropertyChanged();
				}
			}
		}

		public long? MinTCPing
		{
			get => _minTCPing;
			private set
			{
				if (_minTCPing != value)
				{
					_minTCPing = value;
					OnPropertyChanged();
				}
			}
		}

		public long? AverageTCPing
		{
			get
			{
				if (SucceedTCPingCount != 0)
				{
					return Convert.ToInt64(_totalTCPing / SucceedTCPingCount);
				}

				return null;
			}
		}

		#endregion

		public void AddLog(TestResult tRes)
		{
			if (tRes == null)
			{
				return;
			}

			if (tRes.PingResult == null && tRes.TCPingResult == null)
			{
				return;
			}

			TestResults.Add(tRes);
			if (tRes.PingResult != null)
			{
				if (tRes.PingResult.Status == IPStatus.Success)
				{
					++SucceedPingCount;
					_totalPing += tRes.PingResult.RTT;
					LastPing = tRes.PingResult.RTT;

					if (MaxPing == null || MaxPing < LastPing)
					{
						MaxPing = LastPing;
					}

					if (MinPing == null || MinPing > LastPing)
					{
						MinPing = LastPing;
					}
					OnPropertyChanged(nameof(AveragePing));
				}
				else
				{
					++FailedPingCount;
					LastPing = null;
				}
			}

			if (tRes.TCPingResult != null)
			{
				if (tRes.TCPingResult.Status == IPStatus.Success)
				{
					++SucceedTCPingCount;
					_totalTCPing += tRes.TCPingResult.RTT;
					LastTCPing = tRes.TCPingResult.RTT;

					if (MaxTCPing == null || MaxTCPing < LastTCPing)
					{
						MaxTCPing = LastTCPing;
					}

					if (MinTCPing == null || MinTCPing > LastTCPing)
					{
						MinTCPing = LastTCPing;
					}

					OnPropertyChanged(nameof(AverageTCPing));
				}
				else
				{
					++FailedTCPingCount;
					LastTCPing = null;
				}
			}
		}
	}
}