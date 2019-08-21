#pragma warning disable CS0067

using System;
using System.Windows.Input;
using TCPingInfoView.ViewModel;

namespace TCPingInfoView.Commands
{
	public class ShowWindowCommand : ICommand
	{
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (parameter is MainWindowViewModel viewModel)
			{
				viewModel.TriggerShowHide();
			}
		}

		public event EventHandler CanExecuteChanged;
	}
}
