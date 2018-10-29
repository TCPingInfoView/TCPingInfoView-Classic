using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace TCPingInfoView.Util
{
	[DataContract]
	class AppConfig
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

		#endregion

		[IgnoreDataMember]
		public string JsonStr => SimpleJson.SimpleJson.SerializeObject(this);

		[IgnoreDataMember]
		public readonly string Filepath;

		[IgnoreDataMember]
		private static readonly UTF8Encoding Utf8WithoutBom = new UTF8Encoding(false);

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
			DateListHeight = 125;
			IsNotifyClose = true;
			IsShowDateList = true;
			ColumnsOrder = new List<int>(ColumnsCount) { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
			ColumnsWidth = new List<int>(ColumnsCount) { 50, 50, 50, 50, 50, 50, 0, 0, 0, 0, 0, 0 };
		}

		public void Save()
		{
			try
			{
				using (var fileS = new FileStream(Filepath, FileMode.Create, FileAccess.Write))
				{
					using (var sw = new StreamWriter(fileS, Utf8WithoutBom))
					{
						sw.Write(JsonStr);
					}
				}
			}
			catch (Exception)
			{
				// ignored
			}
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
					using (var sr = new StreamReader(Filepath, Utf8WithoutBom))
					{
						var jsonStr = sr.ReadToEnd();
						Load(jsonStr);
					}
				}
			}
			catch (Exception)
			{
				// ignored
			}
		}

		public void Load(string jsonStr)
		{
			var config = SimpleJson.SimpleJson.DeserializeObject<AppConfig>(jsonStr);
			Load(config);
		}

		public void Load(AppConfig config)
		{
			MainFormHeight = config.MainFormHeight;
			MainFormWidth = config.MainFormWidth;
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
		}
	}
}
