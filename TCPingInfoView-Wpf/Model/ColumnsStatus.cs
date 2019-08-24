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

		private int _indexId;
		private int _indexHostname;
		private int _indexIp;
		private int _indexPort;
		private int _indexDescription;
		private int _indexLastPing;
		private int _indexMaxPing;
		private int _indexMinPing;
		private int _indexAveragePing;
		private int _indexLastTcPing;
		private int _indexMaxTcPing;
		private int _indexMinTcPing;
		private int _indexAverageTcPing;

		#region Display

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

		#endregion

		public int IndexId
		{
			get => _indexId;
			set
			{
				if (_indexId != value)
				{
					_indexId = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexHostname
		{
			get => _indexHostname;
			set
			{
				if (_indexHostname != value)
				{
					_indexHostname = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexIp
		{
			get => _indexIp;
			set
			{
				if (_indexIp != value)
				{
					_indexIp = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexPort
		{
			get => _indexPort;
			set
			{
				if (_indexPort != value)
				{
					_indexPort = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexDescription
		{
			get => _indexDescription;
			set
			{
				if (_indexDescription != value)
				{
					_indexDescription = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexLastPing
		{
			get => _indexLastPing;
			set
			{
				if (_indexLastPing != value)
				{
					_indexLastPing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexMaxPing
		{
			get => _indexMaxPing;
			set
			{
				if (_indexMaxPing != value)
				{
					_indexMaxPing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexMinPing
		{
			get => _indexMinPing;
			set
			{
				if (_indexMinPing != value)
				{
					_indexMinPing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexAveragePing
		{
			get => _indexAveragePing;
			set
			{
				if (_indexAveragePing != value)
				{
					_indexAveragePing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexLastTcPing
		{
			get => _indexLastTcPing;
			set
			{
				if (_indexLastTcPing != value)
				{
					_indexLastTcPing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexMaxTcPing
		{
			get => _indexMaxTcPing;
			set
			{
				if (_indexMaxTcPing != value)
				{
					_indexMaxTcPing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexMinTcPing
		{
			get => _indexMinTcPing;
			set
			{
				if (_indexMinTcPing != value)
				{
					_indexMinTcPing = value;
					OnPropertyChanged();
				}
			}
		}

		public int IndexAverageTcPing
		{
			get => _indexAverageTcPing;
			set
			{
				if (_indexAverageTcPing != value)
				{
					_indexAverageTcPing = value;
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
			_displayMaxPing = false;
			_displayMinPing = false;
			_displayAveragePing = true;
			_displayLastTcPing = true;
			_displayMaxTcPing = false;
			_displayMinTcPing = false;
			_displayAverageTcPing = true;

			_indexId = 0;
			_indexHostname = 1;
			_indexIp = 2;
			_indexPort = 3;
			_indexDescription = 4;
			_indexLastPing = 5;
			_indexMaxPing = 6;
			_indexMinPing = 7;
			_indexAveragePing = 8;
			_indexLastTcPing = 9;
			_indexMaxTcPing = 10;
			_indexMinTcPing = 11;
			_indexAverageTcPing = 12;
		}
	}
}
