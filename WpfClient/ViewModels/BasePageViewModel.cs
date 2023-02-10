using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using WpfClient.Models;

namespace WpfClient.ViewModels
{
    public abstract class BasePageViewModel : BindableBase
    {
		private readonly ConcurrentDictionary<string, object> parameters = new ConcurrentDictionary<string, object>();
		public object this[string index]{
            get
            {
                object res;
                if (parameters.TryGetValue(index, out res))
                    return res;
                else return default;
             }
            set 
                => parameters.AddOrUpdate(index, value, (s, o) => value);
		}

		#region Properties
		public bool IsInProgress
		{
			get => Get<bool>();
			protected set => Set(value);
		}
		protected virtual bool IsCanExecute() => !(IsInProgress);

		protected virtual bool IsCanExecute(object obj) => !(IsInProgress);

		#endregion
		#region Events

		public event EventHandler<Window> ModalWindowRequired;

        public event EventHandler<InfoDialogModel> InfoDialogRequired;

        public event EventHandler<ErrorDialogModel> ErrorDialogRequired;

        public event EventHandler<ConfirmDialogModel> ConfirmDialogRequired;

		#endregion

		protected BasePageViewModel()
        {     
        }

        public virtual Task OnPageLoadedAsync(CancellationToken cancellationToken)
        {
            PropertyChanging += ViewModel_PropertyChanging;
            PropertyChanged += ViewModel_PropertyChanged;

            return Task.CompletedTask;
        }

        public virtual Task OnPageUnloadedAsync(CancellationToken cancellationToken)
        {
            PropertyChanging -= ViewModel_PropertyChanging;
            PropertyChanged -= ViewModel_PropertyChanged;

            return Task.CompletedTask;
        }

        public virtual Task LoadDataAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual void ViewModel_PropertyChanging(object sender, PropertyChangingEventArgs e) { }

        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) { }

        protected void RaiseModalWindowRequired(Window modalWindow)
        {
            Debug.Assert(ModalWindowRequired != null);

            ModalWindowRequired?.Invoke(this, modalWindow);
        }

        protected void RaiseInfoDialogRequired(InfoDialogModel model)
        {
            Debug.Assert(InfoDialogRequired != null);

            InfoDialogRequired?.Invoke(this, model);
        }
        protected void RaiseConfirmDialogRequired(ConfirmDialogModel model)
        {
            Debug.Assert(ConfirmDialogRequired != null);

            ConfirmDialogRequired?.Invoke(this, model);
        }

        protected void RaiseErrorDialogRequired(ErrorDialogModel model)
        {
            Debug.Assert(ErrorDialogRequired != null);

            ErrorDialogRequired?.Invoke(this, model);
        }

        protected void OnCommandException(Exception ex) 
            => Debug.WriteLine($"Command failed. Message={ex.Message}");
    }
}
