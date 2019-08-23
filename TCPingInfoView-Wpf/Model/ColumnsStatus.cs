using System;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.Model
{
	[Serializable]
	public class ColumnsStatus : ViewModelBase
	{
		private bool _showId;

		public bool ShowId
		{
			get => _showId;
			set
			{
				if (_showId != value)
				{
					_showId = value;
					OnPropertyChanged();
				}
			}
		}

		public ColumnsStatus()
		{
			_showId = true;
		}
	}
}
