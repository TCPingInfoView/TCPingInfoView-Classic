using System.Collections.ObjectModel;
using System.Windows;
using TCPingInfoView.Model;
using TCPingInfoView.Utils;
using TCPingInfoView.View;

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

		public MainWindow Window { private get; set; }

		public Config Config = new Config();

		public bool AllowPreRelease
		{
			get => Config.AllowPreRelease;
			set
			{
				Config.AllowPreRelease = value;
				OnPropertyChanged();
			}
		}

		public ColumnsStatus ColumnsStatus
		{
			get => Config.ColumnsStatus;
			set
			{
				Config.ColumnsStatus = value;
				OnPropertyChanged();
			}
		}

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
			Window.Visibility = Visibility.Hidden;
		}

		public void ShowWindow(bool notClosing = true)
		{
			Window.ShowWindow(notClosing);
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

		public void CallAllDateTimeChanged()
		{
			foreach (var endPointInfo in EndPointsCollection)
			{
				endPointInfo.CallDateTimeChanged();
			}
		}
	}
}