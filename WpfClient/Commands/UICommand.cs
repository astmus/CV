

using System;
using System.Windows.Input;

namespace WpfClient.Commands
{
    public class UICommand : MvvmHelpers.Commands.Command, ICommand
    {
        public UICommand(Action execute) : base(execute) { }

        public UICommand(Action execute, Func<bool> canExecute) : base(execute, canExecute) { }
    }

    public class UICommand<T> : MvvmHelpers.Commands.Command<T>, ICommand
    {
        public UICommand(Action<T> execute) : base(execute) { }

        public UICommand(Action<T> execute, Func<T, bool> canExecute) : base (execute, canExecute) { }
    }
}
