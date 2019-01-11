using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TCPingInfoView.Model
{
	public class DateTable : INotifyPropertyChanged
	{
		private DateTime date;
		private int latency;

		public DateTime Date
		{
			get => date;
			set
			{
				if (date != value)
				{
					date = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int Latency { 
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

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = @"")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public DateTable()
		{
			date = DateTime.MinValue;
			latency = 0;
		}
	}
}
