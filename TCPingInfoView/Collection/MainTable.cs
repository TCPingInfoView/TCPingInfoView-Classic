using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TCPingInfoView.Collection
{
	public class MainTable : INotifyPropertyChanged
	{
		private int Timeout => MainForm.Timeout;
		private int index;
		private string hostsName;
		private string endpoint;
		private string description;
		private int totalPing = 0;
		private int count = 0;
		private int succeedCount = 0;
		private int failedCount = 0;
		private int? lastPing;
		private int? maxPing;
		private int? minPing;
		private readonly ConcurrentList<DateTable> _info;

		public int Index
		{
			get => index;
			set
			{
				if (index != value)
				{
					index = value;
					NotifyPropertyChanged();
				}
			}
		}

		public string HostsName
		{
			get => hostsName;
			set
			{
				if (hostsName != value)
				{
					hostsName = value;
					NotifyPropertyChanged();
				}
			}
		}

		public string Endpoint
		{
			get => endpoint;
			set
			{
				if (endpoint != value)
				{
					endpoint = value;
					NotifyPropertyChanged();
				}
			}
		}

		public string FailedP
		{
			get
			{
				if (Count == 0)
				{
					return @"0%";
				}
				var fp = (double)FailedCount / Count;
				return fp > 0.0 ? fp.ToString(@"P") : @"0%";
			}
		}

		public string Description
		{
			get => description;
			set
			{
				if (description != value)
				{
					description = value;
					NotifyPropertyChanged();
				}
			}
		}

		public IEnumerable<DateTable> Info => _info;

		public int? Average
		{
			get
			{
				if (SucceedCount != 0)
				{
					return Convert.ToInt32(TotalPing / SucceedCount);
				}
				else
				{
					return null;
				}
			}
		}

		public string SucceedP
		{
			get
			{
				if (Count == 0)
				{
					return @"0%";
				}
				var sp = (double)SucceedCount / Count;
				return sp > 0.0 ? sp.ToString(@"P") : @"0%";
			}
		}

		private int TotalPing
		{
			get => totalPing;
			set
			{
				if (totalPing != value)
				{
					totalPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		private int Count
		{
			get => count;
			set
			{
				if (count != value)
				{
					count = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int SucceedCount
		{
			get => succeedCount;
			set
			{
				if (succeedCount != value)
				{
					succeedCount = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int FailedCount
		{
			get => failedCount;
			set
			{
				if (failedCount != value)
				{
					failedCount = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int? LastPing
		{
			get => lastPing;
			set
			{
				if (value != lastPing)
				{
					lastPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int? MaxPing
		{
			get => maxPing;
			set
			{
				if (maxPing != value)
				{
					maxPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int? MinPing
		{
			get => minPing;
			set
			{
				if (minPing != value)
				{
					minPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = @"")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public MainTable()
		{
			index = 0;
			hostsName = string.Empty;
			endpoint = string.Empty;
			description = string.Empty;
			_info = new ConcurrentList<DateTable>();
		}

		public void AddNewLog(DateTable info)
		{
			_info.Add(info);
			++Count;
			if (info.Latency < Timeout)
			{
				++SucceedCount;
				TotalPing += info.Latency;
				LastPing = info.Latency;

				if (MaxPing < LastPing || MaxPing == null)
				{
					MaxPing = LastPing;
				}

				if (MinPing > LastPing || MinPing == null)
				{
					MinPing = LastPing;
				}
			}
			else
			{
				LastPing = Timeout;
				++FailedCount;
			}
		}

		public void Reset()
		{
			_info.Clear();
			TotalPing = 0;
			Count = 0;
			SucceedCount = 0;
			FailedCount = 0;
			LastPing = null;
			MaxPing = null;
			MinPing = null;
		}
	}
}
