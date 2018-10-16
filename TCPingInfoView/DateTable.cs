using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TCPingInfoView
{
	public class DateTable : INotifyPropertyChanged
	{
		private DateTime date;
		private int latenty;

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

		public int Latenty { get => latenty;
			set
			{
				if (latenty != value)
				{
					latenty = value;
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
			latenty = 0;
		}
	}
}
