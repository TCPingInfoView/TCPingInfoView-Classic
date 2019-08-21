using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TCPingInfoView.JsonConverters;
using TCPingInfoView.Model;
using TCPingInfoViewLib.NetUtils;

namespace TCPingInfoView.ViewModel
{
	[Serializable]
	public class EndPointInfo : ViewModelBase, ICloneable
	{
		private EndPointInfo() : this(0) { }

		public EndPointInfo(int i)
		{
			Index = i;
			_hostname = null;
			_ip = null;
			_port = 443;
			_description = string.Empty;
			_testResults = new ObservableCollection<TestResult>();
			AllowICMP = true;
			AllowTCP = true;
			IsRememberIp = true;
			Reset();
		}

		private void Reset()
		{
			TestResults.Clear();

			_totalPing = 0;
			SucceedPingCount = 0;
			FailedPingCount = 0;
			LastPing = null;
			MaxPing = null;
			MinPing = null;
			OnPropertyChanged(nameof(AveragePing));

			_totalTCPing = 0;
			SucceedTCPingCount = 0;
			FailedTCPingCount = 0;
			LastTCPing = null;
			MaxTCPing = null;
			MinTCPing = null;
			OnPropertyChanged(nameof(AverageTCPing));
		}

		public async void ResetAsync()
		{
			Reset();
			await Task.Delay(0);
		}

		public object Clone()
		{
			if (IsRememberIp)
			{
				return new EndPointInfo(Index)
				{
					Hostname = null,
					Ip = Ip,
					Port = Port,
					Description = Description,
					AllowTCP = AllowTCP,
					AllowICMP = AllowICMP,
					IsRememberIp = IsRememberIp
				};
			}
			return new EndPointInfo(Index)
			{
				Hostname = Hostname,
				Ip = null,
				Port = Port,
				Description = Description,
				AllowTCP = AllowTCP,
				AllowICMP = AllowICMP,
				IsRememberIp = IsRememberIp
			};
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

		public int Index { get; set; }

		public bool IsRememberIp { get; set; }

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

		[JsonConverter(typeof(IPAddressConverter))]
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

		[JsonIgnore]
		public BigInteger IpLong => Ip.ToInteger();

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

		[JsonIgnore]
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

		public bool AllowICMP { get; set; }

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		public bool AllowTCP { get; set; }

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		[JsonIgnore]
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

		private void AddLog(TestResult tRes)
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

		private async Task<TestResult> PingEndPoint(CancellationToken ct, Config config)
		{
			if (Hostname == null && Ip != null)
			{
				var hostname = await DnsQuery.GetHostNameAsync(Ip, ct, config.ReverseDNSTimeout);
				Hostname = hostname ?? Ip.ToString();
			}
			else if (Hostname != null && Ip == null)
			{
				Ip = await DnsQuery.GetIpAsync(Hostname, ct, config.DNSTimeout);
			}
			else if (Hostname == null && Ip == null)
			{
				return null;
			}

			if (Ip == null)
			{
				return null;
			}

			var res = new TestResult
			{
				Time = DateTime.Now
			};

			if (AllowICMP)
			{
				res.PingResult = await NetTest.ICMPingAsync(Ip, config.PingTimeout, ct);
			}

			if (AllowTCP)
			{
				res.TCPingResult = await NetTest.TCPingAsync(Ip, Port, config.TCPingTimeout, ct);
			}

			return res;
		}

		public async void PingOne(CancellationToken ct, Config config)
		{
			var res = await PingEndPoint(ct, config);
			if (!ct.IsCancellationRequested)
			{
				AddLog(res);
			}
		}
	}
}