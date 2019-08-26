using System;
using System.Globalization;
using System.Windows.Data;

namespace TCPingInfoView.ValueConverter
{
	public sealed class DoubleToPercentageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double p)
			{
				return p > 0.0 ? p.ToString(@"P") : @"0%";
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
