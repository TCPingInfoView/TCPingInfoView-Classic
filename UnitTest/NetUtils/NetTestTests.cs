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

			var cts = new CancellationTokenSource();
			cts.Cancel();

			var res0 = await NetTest.TCPingAsync(null);
			var res1 = await NetTest.TCPingAsync(ipv4, 80, 0);
			var res2 = await NetTest.TCPingAsync(ipv4, 80, 10000);
			var res3 = await NetTest.TCPingAsync(ipv4, 3389);
			var res4 = await NetTest.TCPingAsync(ipv6, 3389);
			var res5 = await NetTest.TCPingAsync(ipv6, cts, 3389);

			Assert.AreEqual(res0.Status, IPStatus.BadDestination);
			Assert.AreEqual(res1.Status, IPStatus.TimedOut);
			Assert.AreEqual(res2.Status, IPStatus.BadDestination);
			Assert.AreEqual(res3.Status, IPStatus.Success);
			Assert.AreEqual(res4.Status, IPStatus.Success);
			Assert.IsNull(res5);
		}

		[TestMethod]
		public async Task ICMPingAsyncTest()
		{
			var ipv4 = IPAddress.Loopback;
			var ipv6 = IPAddress.IPv6Loopback;
			var ip1 = IPAddress.Parse(@"172.0.0.1");

			var cts = new CancellationTokenSource();
			cts.Cancel();

			var res0 = await NetTest.ICMPingAsync(ipv4, 300);
			var res1 = await NetTest.ICMPingAsync(ipv6, 300);
			var res2 = await NetTest.ICMPingAsync(ip1, 300);
			var res3 = await NetTest.ICMPingAsync(null);
			var res4 = await NetTest.ICMPingAsync(ip1, cts, 300);

			Assert.AreEqual(res0.Status, IPStatus.Success);
			Assert.AreEqual(res1.Status, IPStatus.Success);
			Assert.AreEqual(res2.Status, IPStatus.TimedOut);

			Assert.AreEqual(res3.Status, IPStatus.BadDestination);
			Assert.IsNull(res4);
		}
	}
}