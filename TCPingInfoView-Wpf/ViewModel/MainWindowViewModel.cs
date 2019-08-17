using System.Collections.ObjectModel;

namespace TCPingInfoView.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			_endpointsCollection = new ObservableCollection<EndPointInfo>();
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
	}
}