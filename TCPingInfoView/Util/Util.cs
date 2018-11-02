using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPingInfoView.Collection;
using TCPingInfoView.NetUtils;

namespace TCPingInfoView.Util
{
	public static class Util
	{
		public static Data StringLine2Data(string line)
		{
			var s = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length < 1)
			{
				return null;
			}

			string hostname;
			IPAddress ip = null;
			var port = 443;

			var sp = s[0].Split(':');
			if (sp.Length == 1 || sp.Length == 2)
			{
				hostname = sp[0];
				if (IPFormatter.IsIPv4Address(hostname))
				{
					ip = IPAddress.Parse(hostname);
				}
			}
			else
			{
				return null;
			}
			if (sp.Length == 2)
			{
				try
				{
					port = Convert.ToInt32(sp[1]);
				}
				catch
				{
					return null;
				}

				if (!IPFormatter.IsPort(port))
				{
					return null;
				}
			}


			var res = new Data
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

		public static ConcurrentList<Data> ToData(IEnumerable<string> sl)
		{
			var data = new ConcurrentList<Data>();
			foreach (var line in sl)
			{
				var l = StringLine2Data(line);
				if (l != null)
				{
					data.Add(l);
				}
			}
			return data;
		}

		public static ConcurrentList<MainTable> ToMainTable(ConcurrentList<Data> data)
		{
			var res = new ConcurrentList<MainTable>();
			for (var i = 0; i < data.Count; ++i)
			{
				var r = new MainTable
				{
					Index = i + 1,
					HostsName = data[i].HostsName,
					//FailedP = @"0%",
					//LastPing = null,
					Description = data[i].Description
				};
				if (data[i].Ip == null)
				{
					r.Endpoint = string.Empty;
				}
				else
				{
					r.Endpoint = $@"{data[i].Ip}:{data[i].Port}";
				}
				res.Add(r);
			}
			return res;
		}

		public static void RemoveCompletedTasks(ref ConcurrentList<Task> tasks)
		{
			tasks.RemoveAll(x => x.IsCompleted);
		}

		public static double GetDpi(this Form form)
		{
			var graphics = form.CreateGraphics();
			return graphics.DpiX / 96;
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

		public static void Invoke(this System.Windows.Forms.Control control, MethodInvoker action)
		{
			control.Invoke(action);
		}
	}
}
