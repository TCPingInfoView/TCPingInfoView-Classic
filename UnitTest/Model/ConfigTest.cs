using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using TCPingInfoView.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UnitTest.Model
{
	[TestClass]
	public class ConfigTest
	{
		[TestMethod]
		public void ParseTest()
		{
			var config = new Config();
			var jsonStr = JsonSerializer.Serialize(config);
			Console.WriteLine(jsonStr);
			var configCopy = JsonSerializer.Deserialize<Config>(jsonStr);
			Assert.AreEqual(configCopy.StartHeight, config.StartHeight);
			Assert.AreEqual(configCopy.StartWidth, config.StartWidth);
			Assert.AreEqual(configCopy.StartLeft, config.StartLeft);
			Assert.AreEqual(configCopy.StartTop, config.StartTop);
		}

		[TestMethod]
		public void PerformanceTest()
		{
			const int times = 1000000;
			var config = new Config();
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			for (var i = 0; i < times; ++i)
			{
				JsonSerializer.Serialize(config);
			}
			stopWatch.Stop();
			Console.WriteLine($@".Net Core 3.0 JsonSerializer.Serialize():{Environment.NewLine}{stopWatch.Elapsed.TotalSeconds}s");
			stopWatch.Restart();
			for (var i = 0; i < times; ++i)
			{
				JsonConvert.SerializeObject(config);
			}
			stopWatch.Stop();
			Console.WriteLine($@"Json.Net JsonConvert.SerializeObject():{Environment.NewLine}{stopWatch.Elapsed.TotalSeconds}s");

			var json1 = JsonSerializer.Serialize(config);
			var json2 = JsonConvert.SerializeObject(config);
			stopWatch.Restart();
			for (var i = 0; i < times; ++i)
			{
				JsonSerializer.Deserialize<Config>(json1);
			}
			stopWatch.Stop();
			Console.WriteLine($@".Net Core 3.0 JsonSerializer.Deserialize():{Environment.NewLine}{stopWatch.Elapsed.TotalSeconds}s");
			stopWatch.Restart();
			for (var i = 0; i < times; ++i)
			{
				JsonConvert.DeserializeObject<Config>(json2);
			}
			stopWatch.Stop();
			Console.WriteLine($@"Json.Net JsonConvert.DeserializeObject():{Environment.NewLine}{stopWatch.Elapsed.TotalSeconds}s");
		}
	}
}
