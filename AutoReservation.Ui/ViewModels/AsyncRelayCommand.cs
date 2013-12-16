﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.Ui.ViewModels
{
	/**
     * From: http://blog.mycupof.net/2012/08/23/mvvm-asyncdelegatecommand-what-asyncawait-can-do-for-uidevelopment/
     */

	public class AsyncRelayCommand : ICommand
	{
		protected readonly Predicate<object> _canExecute;
		protected Func<object, Task> _asyncExecute;

		public AsyncRelayCommand(Func<object, Task> execute)
			: this(execute, null)
		{
		}

		public AsyncRelayCommand(Func<object, Task> asyncExecute,
			Predicate<object> canExecute)
		{
			_asyncExecute = asyncExecute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null)
			{
				return true;
			}

			return _canExecute(parameter);
		}

		public async void Execute(object parameter)
		{
			await ExecuteAsync(parameter);
		}

		protected virtual async Task ExecuteAsync(object parameter)
		{
			await _asyncExecute(parameter);
		}
	}
}