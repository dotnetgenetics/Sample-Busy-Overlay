using System;
using System.Windows.Input;

namespace SampleBusyOverlay
{
    public class DelegateCommand : ICommand
    {
        private readonly Action executeMethod;
        private readonly Func<bool> canExecuteMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, () => true)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");
            if (canExecuteMethod == null)
                throw new ArgumentNullException("canExecuteMethod");

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object stupid)
        {
            return CanExecute();
        }

        public bool CanExecute()
        {
            return canExecuteMethod();
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public void Execute()
        {
            executeMethod();
        }

        public void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
