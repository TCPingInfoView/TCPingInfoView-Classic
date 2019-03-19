using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TCPingInfoView.NetUtils;

namespace TCPingInfoViewTests.NetUtils
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
	}
}