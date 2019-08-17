using System.Net;
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

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			var e1 = new EndPointInfo(1)
			{
				Ip = IPAddress.Loopback,
				Port = 3389,
				Description = @"Windows RDP"
			};
			var e2 = new EndPointInfo(2)
			{
				Hostname = @"bing.com",
				Port = 443,
				Description = @"Bing"
			};
			MainWindowViewModel.EndPointsCollection.Add(e1);
			MainWindowViewModel.EndPointsCollection.Add(e2);
		}

		private void TestButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (var endPointInfo in MainWindowViewModel.EndPointsCollection)
			{
				PingOne(endPointInfo);
			}
		}

		private static async void PingOne(EndPointInfo info)
		{
			var res = await Util.PingEndPoint(info);
			info.AddLog(res);
		}
	}
}
