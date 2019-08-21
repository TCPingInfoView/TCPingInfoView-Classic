using System.Collections.ObjectModel;
using System.Windows;
using TCPingInfoView.Utils;

namespace TCPingInfoView.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			_endpointsCollection = new ObservableCollection<EndPointInfo>();
			_displayedTimerImage = TimerStartImageSource;
			_isTaskStart = false;
			Window = null;
		}

		private const string TimerStartImageSource = @"../Resources/Start.png";
		private const string TimerStopImageSource = @"../Resources/Stop.png";

		public Window Window { private get; set; }

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

		public void HideWindow()
		{
			Window.Visibility = Visibility.Collapsed;
		}

		public void ShowWindow()
		{
			Window.Visibility = Visibility.Visible;
			Win32.UnMinimize(Window);
			if (!Window.Topmost)
			{
				Window.Topmost = true;
				Window.Topmost = false;
			}
		}

		public void TriggerShowHide()
		{
			if (Window == null)
			{
				return;
			}
			if (Window.Visibility == Visibility.Visible)
			{
				HideWindow();
			}
			else
			{
				ShowWindow();
			}
		}
	}
}