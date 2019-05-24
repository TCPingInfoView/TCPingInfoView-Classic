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
			Assert.IsNotNull(DnsQuery.GetIpAsync(host1).Result);
			Assert.AreEqual(DnsQuery.GetIpAsync(host2).Result, IPAddress.IPv6Loopback);
		}
	}
}
