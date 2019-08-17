using Steamworks;
using System;
using System.Diagnostics;

namespace TCPingInfoViewLib.Utils
{
	public static class SteamworksUtils
	{
		public static bool IsLoaded;

		public const uint AppId = 828090;

		public static void Init()
		{
			try
			{
				SteamClient.Init(AppId);
			}
			catch (Exception)
			{
				Debug.WriteLine(@"Failed to load Steam.");
			}
		}

		public static bool UnlockAchievement(string identifier)
		{
			if (IsLoaded)
			{
				foreach (var achievement in SteamUserStats.Achievements)
				{
					if (achievement.Identifier == identifier)
					{
						return !achievement.State && achievement.Trigger();
					}
				}
				return false;
			}
			else
			{
				return false;
			}
		}

		public static bool SetGameInfo(string value, string key = @"status")
		{
			if (IsLoaded)
			{
				return SteamFriends.SetRichPresence(key, value);
			}
			else
			{
				return false;
			}
		}

		public static void SetStatus(int count)
		{
			SetGameInfo($@"正在测试 {count} 个项目");
			SetGameInfo(@"#Status", @"steam_display");
			SetGameInfo($@"{count}", @"Count");
		}

		public static void SetCustomString(int count, string str)
		{
			SetGameInfo($@"正在测试 {count} 个项目");
			SetGameInfo(@"#Custom", @"steam_display");
			SetGameInfo(str, @"CustomString");
		}

		public static void ClearRichPresence()
		{
			SteamFriends.ClearRichPresence();
		}

		public static string GetCurrentGameLanguage()
		{
			return SteamApps.GameLanguage;
		}
	}
}
