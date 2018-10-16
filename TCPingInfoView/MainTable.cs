using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TCPingInfoView
{
	public class MainTable : INotifyPropertyChanged
	{
		private int index;
		private string hostsName;
		private string endpoint;
		private string failedP;
		private int latency;
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
			get => failedP;
			set
			{
				if (failedP != value)
				{
					failedP = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int Latency
		{
			get => latency;
			set
			{
				if (latency != value)
				{
					latency = value;
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
			latency = 0;
			description = string.Empty;
		}
	}
}
