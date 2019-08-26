using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using TCPingInfoViewLib.NetUtils;

namespace UnitTest.NetUtils
{
	[TestClass]
	public class NetTestTests
	{
		[TestMethod]
		public async Task TCPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			var ip1 = IPAddress.Parse(@"172.0.0.1");
			var ip2 = IPAddress.Parse(@"254.6.6.6");
			var ip3 = IPAddress.Parse(@"123.5.96.6");
			var ip4 = IPAddress.Parse(@"0.0.0.0");
			var ip5 = IPAddress.Parse(@"0.0.0.1");
			var ip6 = IPAddress.Parse(@"0.0.0.233");

			var cts = new CancellationTokenSource();
			cts.Cancel();

			var res0 = await NetTest.TCPingAsync(null);
			var res1 = await NetTest.TCPingAsync(ipv4, 80, 0);
			var res2 = await NetTest.TCPingAsync(ipv4, 80, 10000);
			var res3 = await NetTest.TCPingAsync(ipv4, 3389);
			var res4 = await NetTest.TCPingAsync(ipv6, 3389);
			var res5 = await NetTest.TCPingAsync(ipv6, 3389, 10000, cts.Token);
			var res6 = await NetTest.TCPingAsync(ip1, 443);
			var res7 = await NetTest.TCPingAsync(ip2, 443);
			var res8 = await NetTest.TCPingAsync(ip3, 443);
			var res9 = await NetTest.TCPingAsync(ip4, 443);
			var res10 = await NetTest.TCPingAsync(ip5, 443);
			var res11 = await NetTest.TCPingAsync(ip6, 443);

			Assert.AreEqual(res0.Status, IPStatus.BadDestination);
			Assert.AreEqual(res1.Status, IPStatus.TimedOut);
			Assert.AreEqual(res2.Status, IPStatus.BadDestination);
			Assert.AreEqual(res3.Status, IPStatus.Success);
			Assert.AreEqual(res4.Status, IPStatus.Success);
			Assert.IsNull(res5);
			Assert.AreEqual(res6.Status, IPStatus.TimedOut);
			Assert.AreEqual(res7.Status, IPStatus.BadDestination);
			Assert.AreEqual(res8.Status, IPStatus.TimedOut);
			Assert.AreEqual(res9.Status, IPStatus.BadDestination);
			Assert.AreEqual(res10.Status, IPStatus.BadDestination);
			Assert.AreEqual(res11.Status, IPStatus.BadDestination);
		}

		[TestMethod]
		public async Task ICMPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			var ip1 = IPAddress.Parse(@"172.0.0.1");
			var ip2 = IPAddress.Parse(@"254.6.6.6");
			var ip3 = IPAddress.Parse(@"123.5.96.6");
			var ip4 = IPAddress.Parse(@"0.0.0.0");
			var ip5 = IPAddress.Parse(@"0.0.0.1");
			var ip6 = IPAddress.Parse(@"0.0.0.233");

			var cts = new CancellationTokenSource();
			cts.Cancel();

			var res0 = await NetTest.ICMPingAsync(ipv4, 300);
			var res1 = await NetTest.ICMPingAsync(ipv6, 300);
			var res2 = await NetTest.ICMPingAsync(ip1, 300);
			var res3 = await NetTest.ICMPingAsync(null);
			var res4 = await NetTest.ICMPingAsync(ip1, 300, cts.Token);
			var res5 = await NetTest.ICMPingAsync(ip2, 300);
			var res6 = await NetTest.ICMPingAsync(ip3, 300);
			var res7 = await NetTest.ICMPingAsync(ip4, 300);
			var res8 = await NetTest.ICMPingAsync(ip5, 300);
			var res9 = await NetTest.ICMPingAsync(ip6, 300);

			Assert.AreEqual(res0.Status, IPStatus.Success);
			Assert.AreEqual(res1.Status, IPStatus.Success);
			Assert.AreEqual(res2.Status, IPStatus.TimedOut);
			Assert.AreEqual(res3.Status, IPStatus.BadDestination);
			Assert.IsNull(res4);
			Assert.AreEqual(res5.Status, IPStatus.Unknown);
			Assert.AreEqual(res6.Status, IPStatus.TimedOut);
			Assert.AreEqual(res7.Status, IPStatus.BadDestination);
			Assert.AreEqual(res8.Status, IPStatus.Unknown);
			Assert.AreEqual(res9.Status, IPStatus.Unknown);
		}
	}
}