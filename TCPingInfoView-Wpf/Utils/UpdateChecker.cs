using System;
using System.Collections.Generic;
using System.Text.Json;
using TCPingInfoViewLib.GitHubRelease;
using TCPingInfoViewLib.Utils;

namespace TCPingInfoView.Utils
{
	public class UpdateChecker
	{
		public const string Owner = @"HMBSbige";
		public const string Repo = @"TCPingInfoView";

		public string LatestVersionNumber;
		public string LatestVersionUrl;

		public const string Name = @"TCPingInfoView";

		public const string Copyright = @"Copyright © HMBSbige 2019";

		public const string Version = @"2.0.0.0";

		public event EventHandler NewVersionFound;
		public event EventHandler NewVersionFoundFailed;
		public event EventHandler NewVersionNotFound;
		public event EventHandler BeforeCheckVersion;
		public event EventHandler AfterCheckVersion;

		public async void Check(bool isPreRelease)
		{
			try
			{
				BeforeCheckVersion?.Invoke(this, new EventArgs());
				var updater = new GitHubRelease(Owner, Repo);
				var json = await updater.GetAllReleaseAsync();
				var releases = JsonSerializer.Deserialize<List<Release>>(json);
				var latestRelease = VersionUtil.GetLatestRelease(releases, isPreRelease);
				if (VersionUtil.CompareVersion(latestRelease.tag_name, Version) > 0)
				{
					LatestVersionNumber = latestRelease.tag_name;
					LatestVersionUrl = latestRelease.html_url;
					NewVersionFound?.Invoke(this, new EventArgs());
				}
				else
				{
					NewVersionNotFound?.Invoke(this, new EventArgs());
				}
			}
			catch
			{
				NewVersionFoundFailed?.Invoke(this, new EventArgs());
			}
			finally
			{
				AfterCheckVersion?.Invoke(this, new EventArgs());
			}
		}
	}
}
