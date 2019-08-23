using System.Windows;
using TCPingInfoView.Model;

namespace TCPingInfoView.BindingProxy
{
	public class BindingProxyColumnsStatus : Freezable
	{
		#region Overrides of Freezable

		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxyColumnsStatus();
		}

		#endregion

		public ColumnsStatus ColumnsStatus
		{
			get => (ColumnsStatus)GetValue(DataProperty);
			set => SetValue(DataProperty, value);
		}

		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(@"ColumnsStatus", typeof(ColumnsStatus),
						typeof(BindingProxyColumnsStatus));
	}
}
