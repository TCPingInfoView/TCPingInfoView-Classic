using System.Windows;
using TCPingInfoView.Model;
using TCPingInfoView.Utils;

namespace TCPingInfoView.View
{
	public partial class EndPointInfoWindow
	{
		public EndPointInfoWindow(EndPointInfo info, WindowType type)
		{
			InitializeComponent();
			I18NUtil.SetLanguage(Resources, @"EndPointInfoWindow", I18NUtil.CurrentLanguage);

			if (type == WindowType.Edit)
			{
				Title = I18NUtil.GetWindowStringValue(this, @"EditEndPoint");
				Grid.RowDefinitions[^1].Height = new GridLength(0);
			}
			else if (type == WindowType.Add)
			{
				Title = I18NUtil.GetWindowStringValue(this, @"AddEndPoint");
			}

			DataContext = info;
		}

		public enum WindowType
		{
			Edit,
			Add
		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
