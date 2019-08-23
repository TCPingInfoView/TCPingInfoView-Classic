using System.Windows;
using System.Windows.Media;

namespace TCPingInfoView.Utils
{
	public static class ViewUtil
	{
		public static bool IsOnScreen(double x, double y)
		{
			return
					SystemParameters.VirtualScreenLeft <= x &&
					SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth >= x &&
					SystemParameters.VirtualScreenTop <= y &&
					SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight >= y;
		}

		public static T GetParentOfType<T>(this DependencyObject element) where T : DependencyObject
		{
			while (true)
			{
				var type = typeof(T);
				if (element == null)
				{
					return null;
				}

				var parent = VisualTreeHelper.GetParent(element);
				if (parent == null && ((FrameworkElement)element).Parent != null)
				{
					parent = ((FrameworkElement)element).Parent;
				}

				if (parent == null)
				{
					return null;
				}

				if (parent.GetType() == type || parent.GetType().IsSubclassOf(type))
				{
					return parent as T;
				}

				element = parent;
			}
		}
	}
}
