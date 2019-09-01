using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Windows;

namespace UnitTest.Util
{
	[TestClass]
	public class UtilTests
	{
		[TestMethod]
		public void StringLine2DataTest()
		{
			var s1 = @"[2607:f8b0:4007:801::2004]:4154";
			var s2 = @"172.217.14.68:1513";
			var s3 = @"www.google.com:12313";
			var s4 = @"[2607:f8b0:4007:80e::200e]";
			var s5 = @"172.217.14.78";
			var s6 = @"www.youtube.com";
			var s7 = @"2409:8a55:260:1a60:a183:ee9e:98c3:df85:2080";

			var r1 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s1, 1);
			var r2 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s2, 2);
			var r3 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s3, 3);
			var r4 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s4, 4);
			var r5 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s5, 5);
			var r6 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s6, 6);
			var r7 = TCPingInfoView.Utils.Util.StringLine2EndPointInfo(s7, 7);

			Assert.AreEqual(r1.Ip, IPAddress.Parse(@"2607:f8b0:4007:801::2004"));
			Assert.AreEqual(r1.Hostname, null);
			Assert.AreEqual(r1.Port, 4154);
			Assert.AreEqual(r1.Index, 1);

			Assert.AreEqual(r2.Ip, IPAddress.Parse(@"172.217.14.68"));
			Assert.AreEqual(r2.Hostname, null);
			Assert.AreEqual(r2.Port, 1513);
			Assert.AreEqual(r2.Index, 2);

			Assert.AreEqual(r3.Ip, null);
			Assert.AreEqual(r3.Hostname, @"www.google.com");
			Assert.AreEqual(r3.Port, 12313);
			Assert.AreEqual(r3.Index, 3);

			Assert.AreEqual(r4.Ip, IPAddress.Parse(@"2607:f8b0:4007:80e::200e"));
			Assert.AreEqual(r4.Hostname, null);
			Assert.AreEqual(r4.Port, 443);
			Assert.AreEqual(r4.Index, 4);

			Assert.AreEqual(r5.Ip, IPAddress.Parse(@"172.217.14.78"));
			Assert.AreEqual(r5.Hostname, null);
			Assert.AreEqual(r5.Port, 443);
			Assert.AreEqual(r5.Index, 5);

			Assert.AreEqual(r6.Ip, null);
			Assert.AreEqual(r6.Hostname, @"www.youtube.com");
			Assert.AreEqual(r6.Port, 443);
			Assert.AreEqual(r6.Index, 6);

			Assert.AreEqual(r7.Ip, IPAddress.Parse(@"2409:8a55:260:1a60:a183:ee9e:98c3:df85"));
			Assert.AreEqual(r7.Hostname, null);
			Assert.AreEqual(r7.Port, 2080);
			Assert.AreEqual(r7.Index, 7);
		}

		[TestMethod]
		public void CompareVersionTest()
		{
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"2.3.1.0", @"2.3.1") > 0); // wtf??? Be aware that
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"2.0.0.0", @"2.3.1") < 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"1.3.1.0", @"2.3.1") < 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"2.3.1.0", @"1.3.1") > 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"1.2", @"1.3") < 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"1.3", @"1.2") > 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"1.3", @"1.3") == 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"1.2.1", @"1.2") > 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"2.3.1", @"2.4") < 0);
			Assert.IsTrue(TCPingInfoViewLib.Utils.VersionUtil.CompareVersion(@"1.3.2", @"1.3.1") > 0);
		}

		[TestMethod]
		public void IsOnScreenTest()
		{
			Assert.IsTrue(TCPingInfoView.Utils.ViewUtil.IsOnScreen(0, 0));
			Assert.IsTrue(TCPingInfoView.Utils.ViewUtil.IsOnScreen(SystemParameters.PrimaryScreenWidth, SystemParameters.VirtualScreenHeight));
			Assert.IsTrue(TCPingInfoView.Utils.ViewUtil.IsOnScreen(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight));
			Assert.IsFalse(TCPingInfoView.Utils.ViewUtil.IsOnScreen(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight * 2));
			Assert.IsFalse(TCPingInfoView.Utils.ViewUtil.IsOnScreen(SystemParameters.PrimaryScreenWidth, SystemParameters.VirtualScreenHeight * 2));
			Assert.IsFalse(TCPingInfoView.Utils.ViewUtil.IsOnScreen(SystemParameters.VirtualScreenWidth + 1,SystemParameters.VirtualScreenHeight));
			Assert.IsFalse(TCPingInfoView.Utils.ViewUtil.IsOnScreen(SystemParameters.VirtualScreenWidth,SystemParameters.VirtualScreenHeight + 1));
			Assert.IsFalse(TCPingInfoView.Utils.ViewUtil.IsOnScreen(-1, 10));
		}
	}
}