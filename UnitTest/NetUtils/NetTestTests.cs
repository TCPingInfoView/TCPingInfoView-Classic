using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using TCPingInfoView.NetUtils;

namespace UnitTest.NetUtils
{
	[TestClass]
	public class NetTestTests
	{
		[TestMethod]
		public void TCPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;

			var cts = new CancellationTokenSource();
			cts.Cancel();

			Assert.AreEqual(NetTest.TCPingAsync(null).Result.Status, IPStatus.BadDestination);
			Assert.AreEqual(NetTest.TCPingAsync(ipv4, 80, 0).Result.Status, IPStatus.TimedOut);
			Assert.AreEqual(NetTest.TCPingAsync(ipv4, 80, 10000).Result.Status, IPStatus.BadDestination);
			Assert.AreEqual(NetTest.TCPingAsync(ipv4, 3389).Result.Status, IPStatus.Success);
			Assert.AreEqual(NetTest.TCPingAsync(ipv6, 3389).Result.Status, IPStatus.Success);

			Assert.IsNull(NetTest.TCPingAsync(ipv6, cts, 3389).Result);

		}

		[TestMethod]
		public void ICMPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			var ip1 = IPAddress.Parse(@"172.0.0.1");

			var cts = new CancellationTokenSource();
			cts.Cancel();

			Assert.AreEqual(NetTest.ICMPingAsync(ipv4, 300).Result.Status, IPStatus.Success);
			Assert.AreEqual(NetTest.ICMPingAsync(ipv6, 300).Result.Status, IPStatus.Success);

			var res = NetTest.ICMPingAsync(ip1, 300).Result;
			Assert.AreEqual(res.Status, IPStatus.TimedOut);

			Assert.AreEqual(NetTest.ICMPingAsync(null).Result.Status, IPStatus.BadDestination);
			Assert.IsNull(NetTest.ICMPingAsync(ip1, cts, 300).Result);
		}
	}
}