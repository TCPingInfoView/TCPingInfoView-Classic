using System.Threading;
using System.Windows;
using TCPingInfoView.Utils;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.View
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
		private CancellationTokenSource _ctsPingTask = new CancellationTokenSource();

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{

		}

		private void TestButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (var endPointInfo in MainWindowViewModel.EndPointsCollection)
			{
				PingOne(endPointInfo);
			}
		}

		private async void PingOne(EndPointInfo info)
		{
			var res = await Util.PingEndPoint(info, _ctsPingTask);
			info.AddLog(res);
		}

		private void LoadButton_OnClick(object sender, RoutedEventArgs e)
		{
			_ctsPingTask.Cancel();
			//TODO
			//cts_PingTask.Dispose();
			_ctsPingTask = new CancellationTokenSource();
			MainWindowViewModel.EndPointsCollection.Clear();

			var path = Read.GetFilePath();
			if (!string.IsNullOrWhiteSpace(path))
			{
				var rawString = Read.ReadTextFromFile(path);
				var list = Read.ReadEndPointFromString(rawString);
				foreach (var info in list)
				{
					MainWindowViewModel.EndPointsCollection.Add(info);
				}
			}
		}
	}
}
