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
			var achievement = new Achievement(SteamWorksClient, 0);
			return !achievement.State && achievement.Trigger();
		}

		public static bool SetGameInfo(string value, string key = @"status")
		{
			return SteamWorksClient.User.SetRichPresence(key, value);
		}
	}

}
