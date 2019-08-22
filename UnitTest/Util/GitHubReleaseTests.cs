using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TCPingInfoView.Utils;
using TCPingInfoViewLib.GitHubRelease;

namespace UnitTest.Util
{
	[TestClass]
	public class GitHubReleaseTests
	{
		[TestMethod]
		public async Task GetAllReleaseTest()
		{
			var updater = new GitHubRelease(UpdateChecker.Owner, UpdateChecker.Repo);
			var json = await updater.GetAllReleaseAsync();

			var o = JsonSerializer.Deserialize<List<Release>>(json);
			foreach (var release in o)
			{
				Console.WriteLine(release.tag_name);
				Console.WriteLine(release.published_at);
			}
		}

		[TestMethod]
		public async Task GetLatestReleaseTest()
		{
			var updater = new GitHubRelease(UpdateChecker.Owner, UpdateChecker.Repo);
			var json = await updater.GetLatestAsync();

			var release = JsonSerializer.Deserialize<Release>(json);

			Console.WriteLine(release.tag_name);
			Console.WriteLine(release.body);
		}

		[TestMethod]
		public async Task GetLatestTest()
		{
			var updater = new GitHubRelease(UpdateChecker.Owner, UpdateChecker.Repo);
			var json = await updater.GetAllReleaseAsync();
			var releases = JsonSerializer.Deserialize<List<Release>>(json);
			Console.WriteLine(TCPingInfoViewLib.Utils.VersionUtil.GetLatestRelease(releases, false).tag_name);
		}
	}
}
