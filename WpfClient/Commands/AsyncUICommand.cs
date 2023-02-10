

using System;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfClient.ViewModels;

namespace WpfClient.Commands
{
    public class AsyncUICommand : MvvmHelpers.Commands.AsyncCommand, ICommand
    {
        private readonly Action<Exception> onException;

        public AsyncUICommand(
            Func<Task> execute,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null
        ) : base(
            execute,
            canExecute,
            onException
        )
        {
            this.onException = onException;
        }

        void ICommand.Execute(object parameter)
        {
            BackgroundTask.QueueTask(async (cancellationToken) =>
            {
                try
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteAsync();
                    }
                }
                catch (Exception ex)
                {
                    
                if (onException != null)
                    {
                        onException.Invoke(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
            });
        }
    }

    public class AsyncUICommand<T> : MvvmHelpers.Commands.AsyncCommand<T>, MvvmHelpers.Interfaces.IAsyncCommand<T>, ICommand
    {
        private readonly Action<Exception> onException;

        public AsyncUICommand(
            Func<T, Task> execute,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null
        ) : base(
            execute,
            canExecute,
            onException
        )
        {
            this.onException = onException;
        }

        void ICommand.Execute(object parameter)
        {
            BackgroundTask.QueueTask(async (cancellationToken) =>
            {
                try
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteAsync((T)parameter);
                    }
                }
                catch (Exception ex)
                {
                    if (onException != null)
                    {
                        onException.Invoke(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
            });
        }
    }
}
