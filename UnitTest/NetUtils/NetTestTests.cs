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
		public void TCPingTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			Assert.IsNull(NetTest.TCPing(ipv4));
			Assert.IsNotNull(NetTest.TCPing(ipv4, 3389));
			Assert.IsNotNull(NetTest.TCPing(ipv6, 3389));
		}

		[TestMethod]
		public void TCPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			var latency = NetTest.TCPingAsync(ipv4).Result;
			Assert.IsNull(latency);
			latency = NetTest.TCPingAsync(ipv4, 3389).Result;
			Assert.IsNotNull(latency);
			latency = NetTest.TCPingAsync(ipv6, 3389).Result;
			Assert.IsNotNull(latency);
		}

		[TestMethod]
		public void ICMPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			var ip1 = IPAddress.Parse(@"172.0.0.1");

			var cts = new CancellationTokenSource();
			cts.Cancel();

			Assert.IsNotNull(NetTest.ICMPingAsync(ipv4, 300).Result);
			Assert.IsNotNull(NetTest.ICMPingAsync(ipv6, 300).Result);

			var res = NetTest.ICMPingAsync(ip1, 300).Result;
			Assert.IsNotNull(res);
			Assert.AreEqual(res.Status, IPStatus.TimedOut);

			Assert.IsNull(NetTest.ICMPingAsync(null).Result);
			Assert.IsNull(NetTest.ICMPingAsync(ip1, cts, 300).Result);
		}
	}
}