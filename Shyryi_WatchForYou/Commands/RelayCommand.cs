﻿using System;
using System.Windows.Input;

namespace Shyryi_WatchForYou.Commands
{
    internal class RelayCommand : ICommand
    {
        // fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        // constructors
        public RelayCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }
        public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        // events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // methods
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
