using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
			AddLanguageMenu();
			SetLanguage();
			LoadConfig();
		}

		public MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
		private CancellationTokenSource _ctsPingTask = new CancellationTokenSource();
		private Config _config = new Config();
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
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{

		}

		private void MainWindow_OnClosed(object sender, EventArgs e)
		{
			SaveConfig();
		}

		private void TestButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (var endPointInfo in MainWindowViewModel.EndPointsCollection)
			{
				if (_ctsPingTask.IsCancellationRequested)
				{
					break;
				}
				endPointInfo.PingOne(_ctsPingTask.Token);
			}
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
				StopPingTask();
				LoadFormRawList();
			}
		}

		private void StopPingTask()
		{
			_ctsPingTask.Cancel();
			_ctsPingTask = new CancellationTokenSource();
		}

		private void LoadConfig()
		{
			_config = Read.LoadConfig();
			if (_config != null)
			{
				Top = _config.StartTop;
				Left = _config.StartLeft;
			}
			else
			{
				_config = new Config();
				WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}

			Height = _config.StartHeight;
			Width = _config.StartWidth;
			Topmost = _config.Topmost;

			_rawEndPointInfo = _config.EndPointInfo;
			LoadFormRawList();
		}

		private void SaveConfig()
		{
			_config.StartTop = Top;
			_config.StartLeft = Left;
			_config.StartHeight = Height;
			_config.StartWidth = Width;
			_config.Topmost = Topmost;
			_config.EndPointInfo = _rawEndPointInfo.Select(info => (EndPointInfo)info.Clone()).ToList();
			Write.SaveConfig(_config);
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
			SizeToContent = SizeToContent.Manual;
		}
	}
}
