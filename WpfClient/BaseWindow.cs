using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using WpfClient.ViewModels;

namespace WpfClient.Windows.Base
{

	public class BaseWindow : Window
	{
		public virtual BasePageViewModel ViewModel => DataContext as BasePageViewModel;

		public BaseWindow(BasePageViewModel viewModel)
		{

			DataContext = viewModel;

			Loaded += Window_Loaded;
			Unloaded += Window_Unloaded;
		}


		protected virtual void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("Window loaded.");

			ViewModel.ModalWindowRequired += ViewModel_ModalWindowRequired;

			BackgroundTask.QueueTask(async (cancellationToken) =>
			{
				try
				{
					await ViewModel.OnPageLoadedAsync(cancellationToken);
					await ViewModel.LoadDataAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"ViewModel load failed. Message={ex.Message}");
				}
			});
		}

		protected virtual void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("Window unloaded.");


			ViewModel.ModalWindowRequired -= ViewModel_ModalWindowRequired;

			BackgroundTask.QueueTask(async (cancellationToken) =>
			{
				try
				{
					await ViewModel.OnPageUnloadedAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"ViewModel unload failed. Message={ex.Message}");
				}
			});
		}



		private void ViewModel_ModalWindowRequired(object _, Window window)
		{
			var owner = this;

			owner.IsEnabled = false;

			window.Owner = owner;
			window.Closed += (_, _) => owner.IsEnabled = true;
			window.Show();
		}
	}

}
