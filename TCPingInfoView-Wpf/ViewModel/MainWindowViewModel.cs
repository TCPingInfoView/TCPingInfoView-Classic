using System.Collections.ObjectModel;

namespace TCPingInfoView.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			_endpointsCollection = new ObservableCollection<EndPointInfo>();
			_displayedTimerImage = TimerStartImageSource;
			_isTaskStart = false;
		}

		private const string TimerStartImageSource = @"../Resources/Start.png";
		private const string TimerStopImageSource = @"../Resources/Stop.png";


		private ObservableCollection<EndPointInfo> _endpointsCollection;
		public ObservableCollection<EndPointInfo> EndPointsCollection
		{
			get => _endpointsCollection;
			set
			{
				if (_endpointsCollection != value)
				{
					_endpointsCollection = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _isTaskStart;
		public bool IsTaskStart
		{
			get => _isTaskStart;
			set
			{
				_isTaskStart = value;
				if (_isTaskStart)
				{
					SetTimerStopImage();
				}
				else
				{
					SetTimerStartImage();
				}
			}
		}

		private string _displayedTimerImage;
		public string DisplayedTimerImage
		{
			get => _displayedTimerImage;
			private set
			{
				if (_displayedTimerImage != value)
				{
					_displayedTimerImage = value;
					OnPropertyChanged();
				}
			}
		}

		private void SetTimerStartImage()
		{
			DisplayedTimerImage = TimerStartImageSource;
		}

		private void SetTimerStopImage()
		{
			DisplayedTimerImage = TimerStopImageSource;
		}
	}
}