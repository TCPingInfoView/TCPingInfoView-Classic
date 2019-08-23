using System;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.Model
{
	[Serializable]
	public class ColumnsStatus : ViewModelBase
	{
		private bool _displayId;

		private bool _displayHostname;
		private bool _displayIp;
		private bool _displayPort;
		private bool _displayDescription;
		private bool _displayLastPing;
		private bool _displayMaxPing;
		private bool _displayMinPing;
		private bool _displayAveragePing;
		private bool _displayLastTcPing;
		private bool _displayMaxTcPing;
		private bool _displayMinTcPing;
		private bool _displayAverageTcPing;

		public bool DisplayId
		{
			get => _displayId;
			set
			{
				if (_displayId != value)
				{
					_displayId = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayHostname
		{
			get => _displayHostname;
			set
			{
				if (_displayHostname != value)
				{
					_displayHostname = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayIp
		{
			get => _displayIp;
			set
			{
				if (_displayIp != value)
				{
					_displayIp = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayPort
		{
			get => _displayPort;
			set
			{
				if (_displayPort != value)
				{
					_displayPort = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayDescription
		{
			get => _displayDescription;
			set
			{
				if (_displayDescription != value)
				{
					_displayDescription = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayLastPing
		{
			get => _displayLastPing;
			set
			{
				if (_displayLastPing != value)
				{
					_displayLastPing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayMaxPing
		{
			get => _displayMaxPing;
			set
			{
				if (_displayMaxPing != value)
				{
					_displayMaxPing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayMinPing
		{
			get => _displayMinPing;
			set
			{
				if (_displayMinPing != value)
				{
					_displayMinPing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayAveragePing
		{
			get => _displayAveragePing;
			set
			{
				if (_displayAveragePing != value)
				{
					_displayAveragePing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayLastTCPing
		{
			get => _displayLastTcPing;
			set
			{
				if (_displayLastTcPing != value)
				{
					_displayLastTcPing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayMaxTCPing
		{
			get => _displayMaxTcPing;
			set
			{
				if (_displayMaxTcPing != value)
				{
					_displayMaxTcPing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayMinTCPing
		{
			get => _displayMinTcPing;
			set
			{
				if (_displayMinTcPing != value)
				{
					_displayMinTcPing = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DisplayAverageTCPing
		{
			get => _displayAverageTcPing;
			set
			{
				if (_displayAverageTcPing != value)
				{
					_displayAverageTcPing = value;
					OnPropertyChanged();
				}
			}
		}


		public ColumnsStatus()
		{
			_displayId = true;
			_displayHostname = true;
			_displayIp = true;
			_displayPort = true;
			_displayDescription = true;
			_displayLastPing = true;
			_displayMaxPing = true;
			_displayMinPing = true;
			_displayAveragePing = true;
			_displayLastTcPing = true;
			_displayMaxTcPing = true;
			_displayMinTcPing = true;
			_displayAverageTcPing = true;
		}
	}
}
