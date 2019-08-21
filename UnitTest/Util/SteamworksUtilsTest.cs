using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks;
using System;
using System.Threading.Tasks;
using TCPingInfoViewLib.Utils;

namespace UnitTest.Util
{
	[TestClass]
	public class SteamworksUtilsTest
	{
		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context)
		{
			SteamClient.OnCallbackException = e =>
			{
				Console.Error.WriteLine(e.Message);
				Console.Error.WriteLine(e.StackTrace);
				Assert.Fail(e.Message);
			};
			SteamClient.Init(SteamworksUtils.AppId);
		}

		[TestMethod]
		public void Test()
		{


		}

		[TestMethod]
		public async Task TestAsync()
		{
			await Task.Delay(1);
		}
	}
}
