using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using TCPingInfoView.Model;
using TCPingInfoView.ViewModel;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UnitTest.Model
{
	[TestClass]
	public class ConfigTest
	{
		[TestMethod]
		public void ConfigParseTest()
		{
			var config = new Config();
			var jsonStr = JsonSerializer.Serialize(config);
			Console.WriteLine(jsonStr);
			var configCopy = JsonSerializer.Deserialize<Config>(jsonStr);
			Assert.AreEqual(configCopy.StartHeight, config.StartHeight);
			Assert.AreEqual(configCopy.StartWidth, config.StartWidth);
			Assert.AreEqual(configCopy.StartLeft, config.StartLeft);
			Assert.AreEqual(configCopy.StartTop, config.StartTop);
			Assert.AreEqual(configCopy.Topmost, config.Topmost);
			Assert.AreEqual(configCopy.Interval, config.Interval);
			Assert.AreEqual(configCopy.FailedBackgroundColor, config.FailedBackgroundColor);
			Assert.AreEqual(configCopy.SuccessForegroundColor, config.SuccessForegroundColor);
			Assert.AreEqual(configCopy.LongPingForegroundColor, config.LongPingForegroundColor);
			Assert.AreEqual(configCopy.LongPingTimeout, config.LongPingTimeout);
			Assert.AreEqual(configCopy.PingTimeout, config.PingTimeout);
			Assert.AreEqual(configCopy.LongTCPingTimeout, config.LongTCPingTimeout);
			Assert.AreEqual(configCopy.TCPingTimeout, config.TCPingTimeout);
			Assert.AreEqual(configCopy.DNSTimeout, config.DNSTimeout);
			Assert.AreEqual(configCopy.ReverseDNSTimeout, config.ReverseDNSTimeout);
			Assert.AreEqual(configCopy.Language, config.Language);
		}

		[TestMethod]
		public void EndPointInfoParseTest()
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			var endPointInfo = new EndPointInfo(1)
			{
				Hostname = @"dns.google",
				Ip = IPAddress.Parse(@"8.8.8.8"),
				Port = 53,
				Description = @"测试 Test"
			};

			var jsonStr = JsonSerializer.Serialize(endPointInfo, options);
			Console.WriteLine(jsonStr);

			var endPointInfoCopy = JsonSerializer.Deserialize<EndPointInfo>(jsonStr);

			Assert.AreEqual(endPointInfoCopy.Index, endPointInfo.Index);
			Assert.AreEqual(endPointInfoCopy.Hostname, endPointInfo.Hostname);
			Assert.AreEqual(endPointInfoCopy.Ip, endPointInfo.Ip);
			Assert.AreEqual(endPointInfoCopy.Port, endPointInfo.Port);
			Assert.AreEqual(endPointInfoCopy.Description, endPointInfo.Description);
			Assert.AreEqual(endPointInfoCopy.AllowICMP, endPointInfo.AllowICMP);
			Assert.AreEqual(endPointInfoCopy.AllowTCP, endPointInfo.AllowTCP);
		}

		[TestMethod]
		public void PerformanceTest()
		{
			const int times = 100000;
			var config = new Config();
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			for (var i = 0; i < times; ++i)
			{
				JsonSerializer.Serialize(config);
			}
			stopWatch.Stop();
			var t1 = stopWatch.Elapsed.TotalSeconds;
			Console.WriteLine($@".Net Core 3.0 JsonSerializer.Serialize():{Environment.NewLine}{t1}s");
			stopWatch.Restart();
			for (var i = 0; i < times; ++i)
			{
				JsonConvert.SerializeObject(config);
			}
			stopWatch.Stop();
			var t2 = stopWatch.Elapsed.TotalSeconds;
			Console.WriteLine($@"Json.Net JsonConvert.SerializeObject():{Environment.NewLine}{t2}s");
			Assert.IsTrue(t1 < t2);

			var json1 = JsonSerializer.Serialize(config);
			var json2 = JsonConvert.SerializeObject(config);
			stopWatch.Restart();
			for (var i = 0; i < times; ++i)
			{
				JsonSerializer.Deserialize<Config>(json1);
			}
			stopWatch.Stop();
			var t3 = stopWatch.Elapsed.TotalSeconds;
			Console.WriteLine($@".Net Core 3.0 JsonSerializer.Deserialize():{Environment.NewLine}{t3}s");
			stopWatch.Restart();
			for (var i = 0; i < times; ++i)
			{
				JsonConvert.DeserializeObject<Config>(json2);
			}
			stopWatch.Stop();
			var t4 = stopWatch.Elapsed.TotalSeconds;
			Console.WriteLine($@"Json.Net JsonConvert.DeserializeObject():{Environment.NewLine}{t4}s");
			Assert.IsTrue(t3 < t4);
		}
	}
}
