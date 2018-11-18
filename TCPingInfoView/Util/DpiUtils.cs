using Microsoft.Win32;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TCPingInfoView.Util
{
	public static class DpiUtils
	{
		#region struct

		public enum DpiType
		{
			Effective = 0,
			Angular = 1,
			Raw = 2,
		}

		private struct SuggestedBoundsRect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		#endregion

		#region WinAPI

		private const int WM_DPICHANGED = 0x02E0;

		[DllImport(@"user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

		[DllImport(@"user32.dll")]
		private static extern int GetDpiForWindow(IntPtr hWnd);

		[DllImport(@"user32.dll")]
		private static extern bool SetProcessDPIAware();

		[DllImport(@"User32.dll")]
		private static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);

		[DllImport(@"Shcore.dll")]
		private static extern IntPtr GetDpiForMonitor([In] IntPtr hMonitor, [In] DpiType dpiType, [Out] out uint dpiX, [Out] out uint dpiY);

		#endregion

		#region GetDpi

		public static double GetFormDpi(Form form)
		{
			return GetDpiForWindow(form.Handle) / 96.0;
		}

		public static double GetDeviceDpi(this System.Windows.Forms.Control form)
		{
			return form.DeviceDpi / 96.0;
		}

		#endregion

		public static void GetScreenDpi(this Screen screen, DpiType dpiType, out uint dpiX, out uint dpiY)
		{
			var pnt = new Point(screen.Bounds.Left + 1, screen.Bounds.Top + 1);
			var mon = MonitorFromPoint(pnt, 2);
			GetDpiForMonitor(mon, dpiType, out dpiX, out dpiY);
		}

		public static void ChangeFormDpi(Form form, double newDpi)
		{
			var reportedDpi = Convert.ToInt32(form.GetDeviceDpi() * 96);
			var trueDpi = Convert.ToInt32(newDpi * 96);

			if (reportedDpi == trueDpi)
			{
				return;
			}

			var wParam = (trueDpi << 16) | (trueDpi & 0xffff);
			var dpiRatio = trueDpi / (double)reportedDpi;
			var suggestedBounds = new SuggestedBoundsRect
			{
				Left = form.Location.X,
				Top = form.Location.Y,
				Right = form.Location.X + (int)(form.Width * dpiRatio),
				Bottom = form.Location.Y + (int)(form.Height * dpiRatio)
			};

			try
			{
				var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(suggestedBounds));
				Marshal.StructureToPtr(suggestedBounds, ptr, false);
				SendMessage(form.Handle, WM_DPICHANGED, wParam, ptr);
				Marshal.FreeHGlobal(ptr);
			}
			catch (Exception e)
			{
				Trace.WriteLine($@"Exception when initializing per-monitor DPI: {e}");
			}
		}

		[Obsolete]
		public static void SetDPIAwareOld()
		{
			if (Environment.OSVersion.Version.Major >= 6)
			{
				SetProcessDPIAware();
			}
		}

		#region CheckEnvironment

		private static bool HasHighDpiScreen()
		{
			foreach (var screen in Screen.AllScreens)
			{
				screen.GetScreenDpi(DpiType.Effective, out var x, out var y);
				if (x > 96)
				{
					return true;
				}
			}
			return false;
		}

		private static bool IsRequiredNewDPISystem()
		{
			return Environment.OSVersion.Version.CompareTo(new Version(10, 0, 15063, 0)) >= 0;
		}

		private static bool IsDotNetAbove47()
		{
			const string subKey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
			using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subKey))
			{
				if (ndpKey?.GetValue(@"Release") != null)
				{
					return (int)ndpKey.GetValue(@"Release") >= 460798;
				}

				return false;
			}
		}

		private static bool IsNewDpiAwarenessOn()
		{
			NameValueCollection SettingsCollection = null;
			try
			{
				SettingsCollection = ConfigurationManager.GetSection(@"System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection;
			}
			catch
			{
				// ignored
			}

			if (SettingsCollection != null)
			{
				if (SettingsCollection.Get(@"DpiAwareness") == @"PerMonitorV2")
				{
					return true;
				}
			}

			return false;
		}

		public static bool CheckHighDpiEnvironment()
		{
			try
			{
				if (HasHighDpiScreen() && (!IsDotNetAbove47() || !IsNewDpiAwarenessOn() || !IsRequiredNewDPISystem()))
				{
					return false;
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}
}
