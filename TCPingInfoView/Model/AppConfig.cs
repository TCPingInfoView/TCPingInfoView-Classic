using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using TCPingInfoView.Forms;
using TCPingInfoView.Util;

namespace TCPingInfoView.Model
{
	[DataContract]
	internal class AppConfig
	{
		#region DataMember

		[DataMember(Name = @"MainFormHeight")]
		public int MainFormHeight;

		[DataMember(Name = @"MainFormWidth")]
		public int MainFormWidth;

		[DataMember(Name = @"DateListHeight")]
		public int DateListHeight;

		[DataMember(Name = @"IsNotifyClose")]
		public bool IsNotifyClose;

		[DataMember(Name = @"IsShowDateList")]
		public bool IsShowDateList;

		[DataMember(Name = @"ColumnsOrder")]
		public List<int> ColumnsOrder;

		[DataMember(Name = @"ColumnsWidth")]
		public List<int> ColumnsWidth;

		[DataMember(Name = @"StartPositionLeft")]
		public int StartPositionLeft;

		[DataMember(Name = @"StartPositionTop")]
		public int StartPositionTop;

		[DataMember(Name = @"TCPingOptions")]
		public TCPingOptions TCPingOptions;

		#endregion

		[IgnoreDataMember]
		public string JsonStr => SimpleJson.SerializeObject(this);

		[IgnoreDataMember]
		public readonly string Filepath;

		[IgnoreDataMember]
		private const int ColumnsCount = MainForm.ColumnsCount;

		public AppConfig(string filepath) : this()
		{
			Filepath = filepath;
		}

		private AppConfig()
		{
			MainFormHeight = 717;
			MainFormWidth = 928;
			StartPositionLeft = -1000;
			StartPositionTop = -1000;
			DateListHeight = 125;
			IsNotifyClose = true;
			IsShowDateList = true;
			ColumnsOrder = new List<int>(ColumnsCount) { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
			ColumnsWidth = new List<int>(ColumnsCount) { 50, 50, 50, 50, 50, 50, 0, 0, 0, 0, 0, 0 };
			TCPingOptions = new TCPingOptions();
		}

		public void Save()
		{
			Write.WriteToFile(Filepath, JsonStr);
		}

		public void Load()
		{
			try
			{
				if (!File.Exists(Filepath))
				{
					Save();
				}
				else
				{
					var jsonStr = Read.ReadTextFromFile(Filepath);
					Load(jsonStr);
				}
			}
			catch (Exception)
			{
				// ignored
			}
		}

		public void Load(string jsonStr)
		{
			var config = SimpleJson.DeserializeObject<AppConfig>(jsonStr, new JsonSerializerStrategy());
			Load(config);
		}

		public void Load(AppConfig config)
		{
			MainFormHeight = config.MainFormHeight;
			MainFormWidth = config.MainFormWidth;
			StartPositionLeft = config.StartPositionLeft;
			StartPositionTop = config.StartPositionTop;
			DateListHeight = config.DateListHeight;
			IsNotifyClose = config.IsNotifyClose;
			IsShowDateList = config.IsShowDateList;
			for (var i = 0; i < config.ColumnsOrder.Count; ++i)
			{
				ColumnsOrder[i] = config.ColumnsOrder[i];
			}
			for (var i = 0; i < config.ColumnsWidth.Count; ++i)
			{
				ColumnsWidth[i] = config.ColumnsWidth[i];
			}
			TCPingOptions = config.TCPingOptions;
		}

		private class JsonSerializerStrategy : PocoJsonSerializerStrategy
		{
			public override object DeserializeObject(object value, Type type)
			{
				if (type == typeof(Color))
				{
					dynamic color = SimpleJson.DeserializeObject(value.ToString());
					var r = Convert.ToInt32(color["R"]);
					var g = Convert.ToInt32(color["G"]);
					var b = Convert.ToInt32(color["B"]);
					var a = Convert.ToInt32(color["A"]);
					return Color.FromArgb(a, r, g, b);
				}
				return base.DeserializeObject(value, type);
			}
		}
	}


}
