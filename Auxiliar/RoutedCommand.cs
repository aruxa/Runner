using System;
using System.Windows.Input;

namespace runner.Auxiliar
{

    public class Command : ICommand
    {
        readonly Func<object, bool> canExecute;
        readonly Action<object> executeAction;

        public Command(Action<object> executeAction)
            : this(executeAction, null)
        {
        }

        public Command(Action<object> executeAction, Func<object, bool> canExecute)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool result = true;
            Func<object, bool> canExecuteHandler = canExecute;
            if (canExecuteHandler != null)
            {
                result = canExecuteHandler(parameter);
            }

            return result;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
