using System;
using System.Globalization;
using System.Windows.Data;
using TCPingInfoView.Utils;

namespace TCPingInfoView.ValueConverter
{
	public sealed class DateTimeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime time)
			{
				return time.ToString(new CultureInfo(I18NUtil.CurrentLanguage ?? I18NUtil.GetLanguage()));
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
