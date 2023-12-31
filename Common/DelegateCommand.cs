﻿using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CanvasPractice.Common
{
    public class DelegateCommand : ICommand
    {
        private Action<object> executeAction;
        private Func<object, bool> canExecuteFunc;
        public event EventHandler? CanExecuteChanged;
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        { }
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                return;
            }
            executeAction = execute;
            canExecuteFunc = canExecute;
        }
        public bool CanExecute(object parameter)
        {

            if (canExecuteFunc == null)
            {
                return true;
            }
            return canExecuteFunc(parameter);
        }
        public void Execute(object parameter)
        {
            if (parameter is ComboBox control)
            {
                var prop = ComboBox.TextProperty;
                var binding = BindingOperations.GetBindingExpression(control, prop);
                binding?.UpdateSource();
            }
            if (executeAction == null)
            {
                return;
            }
            executeAction(parameter);
        }
    }
}
