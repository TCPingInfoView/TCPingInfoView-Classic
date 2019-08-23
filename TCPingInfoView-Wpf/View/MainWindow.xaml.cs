using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TCPingInfoView.Model;
using TCPingInfoView.Utils;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.View
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			MainWindowViewModel.Window = this;
			LoadConfig();
			AddLanguageMenu();
			SetLanguage(MainWindowViewModel.Config.Language);
		}

		public MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
		private CancellationTokenSource _ctsPingTask = new CancellationTokenSource();

		private void AddLanguageMenu()
		{
			foreach (var (name, langName) in I18NUtil.SupportLanguage)
			{
				var newMenuItem = new MenuItem
				{
					Header = name
				};
				newMenuItem.Click += (o, args) =>
				{
					SetLanguage(langName);
				};
				LanguageMenu.Items.Add(newMenuItem);
			}
		}

		private void LoadNotifyIconContextMenu()
		{
			NotifyIcon.ContextMenu = new ContextMenu();
			var exitMenuItem = new MenuItem
			{
				Header = I18NUtil.GetWindowStringValue(this, @"Exit")
			};
			exitMenuItem.Click += ExitButton_OnClick;
			NotifyIcon.ContextMenu.Items.Add(exitMenuItem);
		}

		private void SetLanguage(string langName = @"")
		{
			if (string.IsNullOrEmpty(langName))
			{
				langName = I18NUtil.GetLanguage();
			}
			if (Application.LoadComponent(new Uri($@"../I18N/MainWindow.{langName}.xaml", UriKind.Relative)) is ResourceDictionary langRd)
			{
				Resources.MergedDictionaries.Add(langRd);
				if (Resources.MergedDictionaries.Count > 1)
				{
					Resources.MergedDictionaries.RemoveAt(0);
				}
			}
			I18NUtil.SetLanguage(langName);
			LoadNotifyIconContextMenu();
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{

		}

		private void MainWindow_OnClosed(object sender, EventArgs e)
		{
			StopPingTask();
			SaveConfig();
		}

		private async void TestButton_Click(object sender, RoutedEventArgs e)
		{
			PingAll(_ctsPingTask.Token);
			await Task.Delay(0);
		}

		private void LoadButton_OnClick(object sender, RoutedEventArgs e)
		{
			LoadListFromFile();
		}

		private void ToolBar_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender is ToolBar toolBar)
			{
				// Hide grip
				if (toolBar.Template.FindName(@"OverflowGrid", toolBar) is FrameworkElement overflowGrid)
				{
					overflowGrid.Visibility = Visibility.Collapsed;
				}
				if (toolBar.Template.FindName(@"MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
				{
					mainPanelBorder.Margin = new Thickness();
				}
			}
		}

		private void LoadFormRawList(IEnumerable<EndPointInfo> list, bool isLoad = true)
		{
			if (isLoad)
			{
				MainWindowViewModel.EndPointsCollection.Clear();
			}
			var i = MainWindowViewModel.EndPointsCollection.Count;
			foreach (var info in list)
			{
				info.Index = ++i;
				MainWindowViewModel.EndPointsCollection.Add(info);
			}
		}

		private void LoadListFromFile(bool isLoad = true)
		{
			var path = Read.GetFilePath();
			if (!string.IsNullOrWhiteSpace(path))
			{
				var rawString = Read.ReadTextFromFile(path);
				var list = Read.ReadEndPointFromString(rawString);
				SaveConfig();
				StopPingTask();
				LoadFormRawList(list, isLoad);
			}
		}

		private void PingAll(CancellationToken ct)
		{
			foreach (var endPointInfo in MainWindowViewModel.EndPointsCollection)
			{
				if (ct.IsCancellationRequested)
				{
					break;
				}
				endPointInfo.PingOne(ct, MainWindowViewModel.Config);
			}
		}

		private async void StartPingTask(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				PingAll(ct);
				try
				{
					await Task.Delay(MainWindowViewModel.Config.Interval * 1000, ct);
				}
				catch (TaskCanceledException)
				{
					// ignored
				}
			}
		}

		private void StopPingTask()
		{
			_ctsPingTask.Cancel();
			_ctsPingTask = new CancellationTokenSource();
			MainWindowViewModel.IsTaskStart = false;
		}

		private void LoadConfig()
		{
			MainWindowViewModel.Config = Read.LoadConfig();
			if (MainWindowViewModel.Config != null)
			{
				Top = MainWindowViewModel.Config.StartTop;
				Left = MainWindowViewModel.Config.StartLeft;
			}
			else
			{
				MainWindowViewModel.Config = new Config();
				WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}

			Height = MainWindowViewModel.Config.StartHeight;
			Width = MainWindowViewModel.Config.StartWidth;
			Topmost = MainWindowViewModel.Config.Topmost;
			MainWindowViewModel.AllowPreRelease = MainWindowViewModel.Config.AllowPreRelease;

			LoadFormRawList(MainWindowViewModel.Config.EndPointInfo);
		}

		private void SaveConfig()
		{
			MainWindowViewModel.Config.StartTop = Top;
			MainWindowViewModel.Config.StartLeft = Left;
			MainWindowViewModel.Config.StartHeight = Height;
			MainWindowViewModel.Config.StartWidth = Width;
			MainWindowViewModel.Config.Topmost = Topmost;
			MainWindowViewModel.Config.Language = I18NUtil.CurrentLanguage;
			MainWindowViewModel.Config.EndPointInfo = MainWindowViewModel.EndPointsCollection.Select(info => (EndPointInfo)info.Clone()).ToList();
			Write.SaveConfig(MainWindowViewModel.Config);
		}

		//private void ShowBalloonTip(string title, string message)
		//{
		//	NotifyIcon.ShowBalloonTip(title, message, NotifyIcon.Icon, true);
		//}

		private void ExitButton_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ResetButton_OnClick(object sender, RoutedEventArgs e)
		{
			StopPingTask();
			foreach (var endPointInfo in MainWindowViewModel.EndPointsCollection)
			{
				endPointInfo.ResetAsync();
			}
		}

		private void AlwaysOnTop_OnClick(object sender, RoutedEventArgs e)
		{
			Topmost = !Topmost;
		}

		private void AutoSize_OnClick(object sender, RoutedEventArgs e)
		{
			foreach (var column in EndPointDataGrid.Columns)
			{
				column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
			}
			SizeToContent = SizeToContent.Width;
		}

		private void TimerButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (MainWindowViewModel.IsTaskStart)
			{
				StopPingTask();
			}
			else
			{
				StartPingTask(_ctsPingTask.Token);
				MainWindowViewModel.IsTaskStart = true;
			}
		}

		private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
		{
			MainWindowViewModel.HideWindow();
		}

		private void ClearButton_OnClick(object sender, RoutedEventArgs e)
		{
			StopPingTask();
			MainWindowViewModel.EndPointsCollection.Clear();
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e)
		{
			LoadListFromFile(false);
		}

		private void FeedbackMenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			Util.OpenUrl(@"https://github.com/HMBSbige/TCPingInfoView/issues/new");
		}

		private void CheckUpdateMenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			var updater = new UpdateChecker();
			updater.BeforeCheckVersion += (o, _) => { CheckUpdateMenuItem.IsEnabled = false; };
			updater.AfterCheckVersion += (o, _) => { CheckUpdateMenuItem.IsEnabled = true; };
			updater.NewVersionFound += (o, _) =>
			{
				var res = MessageBox.Show($@"{I18NUtil.GetWindowStringValue(this, @"NewVersionFound")}: {updater.LatestVersionNumber}
{I18NUtil.GetWindowStringValue(this, @"AskForUpdates")}",
				UpdateChecker.Name,
				MessageBoxButton.YesNo,
				MessageBoxImage.Information, MessageBoxResult.No);
				if (res == MessageBoxResult.Yes)
				{
					Util.OpenUrl(updater.LatestVersionUrl);
				}
			};
			updater.NewVersionNotFound += (o, _) =>
			{
				MessageBox.Show($@"{I18NUtil.GetWindowStringValue(this, @"NewVersionNotFound")}
{I18NUtil.GetWindowStringValue(this, @"CurrentVersion")}: {UpdateChecker.Version} ≥ {updater.LatestVersionNumber}", UpdateChecker.Name, MessageBoxButton.OK, MessageBoxImage.Information);
			};
			updater.NewVersionFoundFailed += (o, _) =>
			{
				MessageBox.Show(I18NUtil.GetWindowStringValue(this, @"NewVersionFoundFailed"), UpdateChecker.Name, MessageBoxButton.OK, MessageBoxImage.Error);
			};
			updater.Check(MainWindowViewModel.AllowPreRelease);
		}

		private void AboutMenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			if (FindResource(@"ProjectUrl") is string url)
			{
				Util.OpenUrl(url);
			}
		}

		private void AllowPreReleaseMenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			MainWindowViewModel.AllowPreRelease = !MainWindowViewModel.AllowPreRelease;
		}
	}
}
