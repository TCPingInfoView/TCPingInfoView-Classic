using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Numerics;
using TCPingInfoViewLib.NetUtils;

namespace UnitTest.NetUtils
{
	[TestClass]
	public class IPFormatterTests
	{
		[TestMethod]
		public void ToIPEndPointTest()
		{
			var s1 = @"[2607:f8b0:4007:801::2004]:4154";
			var s2 = @"172.217.14.68:1513";
			var s3 = @"www.google.com:12313";
			var s4 = @"[2607:f8b0:4007:80e::200e]";
			var s5 = @"172.217.14.78";
			var s6 = @"www.youtube.com";

			var r1 = IPFormatter.ToIPEndPoint(s1);
			var r2 = IPFormatter.ToIPEndPoint(s2);
			var r3 = IPFormatter.ToIPEndPoint(s3);
			var r4 = IPFormatter.ToIPEndPoint(s4);
			var r5 = IPFormatter.ToIPEndPoint(s5);
			var r6 = IPFormatter.ToIPEndPoint(s6);

			Assert.AreEqual(r1.Address, IPAddress.Parse(@"2607:f8b0:4007:801::2004"));
			Assert.AreEqual(r1.Port, 4154);

			Assert.AreEqual(r2.Address, IPAddress.Parse(@"172.217.14.68"));
			Assert.AreEqual(r2.Port, 1513);

			Assert.IsNull(r3);

			Assert.AreEqual(r4.Address, IPAddress.Parse(@"2607:f8b0:4007:80e::200e"));
			Assert.AreEqual(r4.Port, 443);

			Assert.AreEqual(r5.Address, IPAddress.Parse(@"172.217.14.78"));
			Assert.AreEqual(r5.Port, 443);

			Assert.IsNull(r6);
		}

		[TestMethod]
		public void ToIntegerTest()
		{
			var s1 = IPAddress.Loopback;
			var s2 = IPAddress.Parse(@"172.217.14.68");
			var s3 = IPAddress.IPv6Loopback;
			var s4 = IPAddress.Parse(@"2607:f8b0:4007:80e::200e");

			Assert.AreEqual(s1.ToInteger(), 2130706433);
			Assert.AreEqual(s2.ToInteger(), 2899906116);
			Assert.AreEqual(s3.ToInteger(), BigInteger.One);
			Assert.AreEqual(s4.ToInteger(), BigInteger.Parse(@"50552053919390396235022603938716196878"));
		}

	}
}