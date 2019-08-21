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
			SetLanguage(Config.Language);
		}

		public MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
		private CancellationTokenSource _ctsPingTask = new CancellationTokenSource();
		public Config Config = new Config();
		private IEnumerable<EndPointInfo> _rawEndPointInfo;

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

		private void LoadFormRawList()
		{
			MainWindowViewModel.EndPointsCollection.Clear();
			foreach (var info in _rawEndPointInfo)
			{
				MainWindowViewModel.EndPointsCollection.Add(info);
			}
		}

		private void LoadListFromFile()
		{
			var path = Read.GetFilePath();
			if (!string.IsNullOrWhiteSpace(path))
			{
				var rawString = Read.ReadTextFromFile(path);
				_rawEndPointInfo = Read.ReadEndPointFromString(rawString);
				SaveConfig();
				StopPingTask();
				LoadFormRawList();
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
				endPointInfo.PingOne(ct, Config);
			}
		}

		private async void StartPingTask(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				PingAll(ct);
				try
				{
					await Task.Delay(Config.Interval * 1000, ct);
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
			Config = Read.LoadConfig();
			if (Config != null)
			{
				Top = Config.StartTop;
				Left = Config.StartLeft;
			}
			else
			{
				Config = new Config();
				WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}

			Height = Config.StartHeight;
			Width = Config.StartWidth;
			Topmost = Config.Topmost;

			_rawEndPointInfo = Config.EndPointInfo;
			LoadFormRawList();
		}

		private void SaveConfig()
		{
			Config.StartTop = Top;
			Config.StartLeft = Left;
			Config.StartHeight = Height;
			Config.StartWidth = Width;
			Config.Topmost = Topmost;
			Config.Language = I18NUtil.CurrentLanguage;
			Config.EndPointInfo = _rawEndPointInfo.Select(info => (EndPointInfo)info.Clone()).ToList();
			Write.SaveConfig(Config);
		}

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
	}
}
