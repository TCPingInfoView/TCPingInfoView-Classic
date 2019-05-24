using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TCPingInfoView.Model;
using TCPingInfoView.NetUtils;

namespace TCPingInfoView.Utils
{
	public static class Util
	{
		public static EndPointInfo StringLine2Data(string line)
		{
			var s = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length < 1)
			{
				return null;
			}

			string hostname;
			IPAddress ip;
			var port = 443;

			var sp = IPFormatter.EndPointRegexStr.Match(s[0]).Groups;
			if (sp.Count == 5)
			{
				hostname = string.IsNullOrWhiteSpace(sp[1].Value) ? sp[3].Value : sp[1].Value;
				IPAddress.TryParse(hostname, out ip);

				if (!int.TryParse(string.IsNullOrWhiteSpace(sp[2].Value) ? sp[4].Value : sp[2].Value, out port))
				{
					return null;
				}

				if (!IPFormatter.IsPort(port))
				{
					return null;
				}
			}
			else if (sp.Count == 1)
			{
				var groups = Regex.Match(s[0], @"^\[(.*)\]$").Groups;
				if (groups.Count == 2)
				{
					hostname = groups[1].Value;
					IPAddress.TryParse(hostname, out ip);
				}
				else
				{
					hostname = s[0];
					IPAddress.TryParse(hostname, out ip);
				}
			}
			else
			{
				return null;
			}

			var res = new EndPointInfo
			{
				HostsName = hostname,
				Ip = ip,
				Port = port,
				Description = string.Empty
			};

			if (s.Length == 2)
			{
				res.Description = s[1];
			}

			return res;
		}

		public static Image ResizeImage(Image imgToResize, Size size)
		{
			//获取图片宽度
			var sourceWidth = imgToResize.Width;
			//获取图片高度
			var sourceHeight = imgToResize.Height;

			//计算宽度的缩放比例
			var nPercentW = size.Width / (float)sourceWidth;
			//计算高度的缩放比例
			var nPercentH = size.Height / (float)sourceHeight;

			var nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;
			//期望的宽度
			var destWidth = (int)(sourceWidth * nPercent);
			//期望的高度
			var destHeight = (int)(sourceHeight * nPercent);

			var b = new Bitmap(destWidth, destHeight);
			var g = Graphics.FromImage(b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			//绘制图像
			g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
			g.Dispose();
			return b;
		}

		public static void AutoColumnSize(DataGridView drv, DataGridViewAutoSizeColumnMode mode)
		{
			var displayedIndex = new List<int>();
			for (var i = 0; i < drv.Columns.Count; ++i)
			{
				for (var j = 0; j < drv.Columns.Count; ++j)
				{
					if (drv.Columns[j].DisplayIndex == i && drv.Columns[j].Visible)
					{
						displayedIndex.Add(j);
					}
				}
			}

			for (var i = 0; i < displayedIndex.Count - 1; ++i)
			{
				var index = displayedIndex[i];
				var column = drv.Columns[index];
				column.AutoSizeMode = mode;
				var widthCol = column.Width;
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
				column.Width = widthCol;
			}
		}

		public static bool IsOnScreen(Point topLeft, Form form)
		{
			var screens = Screen.AllScreens;
			return (from screen in screens let formRectangle = new Rectangle(topLeft.X, topLeft.Y, form.Width, form.Height) where screen.WorkingArea.IntersectsWith(formRectangle) select screen).Any();
		}
	}
}
