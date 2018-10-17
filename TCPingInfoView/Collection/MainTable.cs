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
		private string failedP;
		private string description;

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
				var fp = (double)FailedCount / Count;
				return fp > 0.0 ? fp.ToString(@"P") : @"0%";
			}

			set
			{
				if (failedP != value)
				{
					failedP = value;
					NotifyPropertyChanged();
				}
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
			failedP = string.Empty;
			description = string.Empty;
			_info = new ConcurrentList<DateTable>();
		}

		private readonly ConcurrentList<DateTable> _info;
		public IEnumerable<DateTable> Info => _info;

		private double totalping = 0;
		private int count = 0;
		private int succeedCount = 0;
		private int failedCount = 0;
		private double lastPing = 0;
		private double maxPing = 0;
		private double minPing = double.MaxValue;

		public double Average => Totalping / SucceedCount;
		public double SucceedP => (double)SucceedCount / Count;

		public double Totalping
		{
			get => totalping;
			set
			{
				if (Math.Abs(totalping - value) > 0.0)
				{
					totalping = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int Count
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

		public int LastPing
		{
			get => Convert.ToInt32(lastPing);
			set
			{
				if (Math.Abs(lastPing - value) > 0.0)
				{
					lastPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		public double MaxPing
		{
			get => maxPing;
			set
			{
				if (Math.Abs(maxPing - value) > 0.0)
				{
					maxPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		public double MinPing
		{
			get => minPing;
			set
			{
				if (Math.Abs(minPing - value) > 0.0)
				{
					minPing = value;
					NotifyPropertyChanged();
				}
			}
		}

		public void AddNewLog(DateTable info)
		{
			_info.Add(info);
			++Count;
			if (info.Latenty < Timeout)
			{
				++SucceedCount;
				Totalping += info.Latenty;
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
				LastPing = Timeout;
				++FailedCount;
			}
		}
	}
}
