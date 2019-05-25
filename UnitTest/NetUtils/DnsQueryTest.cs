using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TCPingInfoView.NetUtils;

namespace UnitTest.NetUtils
{
	[TestClass]
	public class DnsQueryTest
	{
		[TestMethod]
		public void GetIpTest()
		{
			var host1 = @"www.google.com";
			var host2 = @"ip6-localhost";
			var ip1 = IPAddress.Loopback.ToString();
			var ip2 = IPAddress.IPv6Loopback.ToString();
			Assert.IsNotNull(DnsQuery.GetIpAsync(host1).Result);
			Assert.AreEqual(DnsQuery.GetIpAsync(host2).Result, IPAddress.IPv6Loopback);
			Assert.AreEqual(DnsQuery.GetIpAsync(ip1).Result, IPAddress.Loopback);
			Assert.AreEqual(DnsQuery.GetIpAsync(ip2).Result, IPAddress.IPv6Loopback);
		}

		[TestMethod]
		public void GetNameTest()
		{
			var ip1 = IPAddress.Parse(@"1.1.1.1");
			var ip2 = IPAddress.Parse(@"8.8.8.8");
			var ip3 = IPAddress.Parse(@"9.9.9.9");
			Assert.AreEqual(DnsQuery.GetHostNameAsync(ip1).Result, @"one.one.one.one");
			Assert.AreEqual(DnsQuery.GetHostNameAsync(ip2).Result, @"google-public-dns-a.google.com");
			Assert.AreEqual(DnsQuery.GetHostNameAsync(ip3).Result, @"dns.quad9.net");
		}
	}
}
