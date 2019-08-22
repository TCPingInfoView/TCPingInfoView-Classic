using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using TCPingInfoView.View;

namespace TCPingInfoView.ValueConverter
{
	public class PingToForegroundBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter is MainWindow window)
			{
				if (value is long ping)
				{
					if (ping< window.MainWindowViewModel.Config.LongPingTimeout)
					{
						return new SolidColorBrush(window.MainWindowViewModel.Config.SuccessForegroundColor);
					}
					return new SolidColorBrush(window.MainWindowViewModel.Config.LongPingForegroundColor);
				}
			}
			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
