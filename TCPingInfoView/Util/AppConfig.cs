using System;
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

		#endregion

		[IgnoreDataMember]
		public string JsonStr => SimpleJson.SimpleJson.SerializeObject(this);

		[IgnoreDataMember]
		public readonly string Filepath;

		[IgnoreDataMember]
		private static readonly UTF8Encoding Utf8WithoutBom = new UTF8Encoding(false);

		public AppConfig(string filepath) : this()
		{
			Filepath = filepath;
		}

		private AppConfig()
		{
			MainFormHeight = 717;
			MainFormWidth = 928;
			DateListHeight = 125;
			IsNotifyClose = false;
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
		}
	}
}
