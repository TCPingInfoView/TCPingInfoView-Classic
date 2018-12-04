using Facepunch.Steamworks;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using TCPingInfoView.Util;

namespace TCPingInfoView.Steamworks
{
	public static class SteamManager
	{
		public static bool IsLoaded;

		private const uint AppId = 828090;

		public static Client SteamWorksClient;
		private static Auth.Ticket _ticket;

		public static bool LaunchedBySteam()
		{
			var parentName = Process.GetCurrentProcess().Parent()?.ProcessName;
			return !string.IsNullOrWhiteSpace(parentName) && parentName.Equals(@"Steam");
		}

		//I noticed if the API dll is messed up the process just crashes.
		[HandleProcessCorruptedStateExceptions]
		public static void Init()
		{
			try
			{
				SteamWorksClient = new Client(AppId);
				if (SteamWorksClient == null)
				{
					IsLoaded = false;
					return;
				}

				_ticket = SteamWorksClient.Auth.GetAuthSessionTicket();
				IsLoaded = _ticket != null;
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
				foreach (var achievement in SteamWorksClient.Achievements.All)
				{
					if (achievement.Id == identifier)
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
				return SteamWorksClient.User.SetRichPresence(key, value);
			}
			else
			{
				return false;
			}
		}

		public static void SetStatus(int Count)
		{
			SetGameInfo($@"正在测试 {Count} 个项目");
			SetGameInfo(@"#Status", @"steam_display");
			SetGameInfo($@"{Count}", @"Count");
		}

		public static void SetCustomString(string str)
		{
			SetGameInfo(@"#Custom", @"steam_display");
			SetGameInfo(str, @"CustomString");
		}
	}

}
