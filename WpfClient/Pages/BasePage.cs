using ModernWpf.Controls;

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

using WpfClient.Commands;
using WpfClient.Models;
using WpfClient.ViewModels;

namespace WpfClient.Pages
{
	public class BasePage : ModernWpf.Controls.Page
	{
		private ContentDialog _contentDialog = null;
	

		public virtual BasePageViewModel ViewModel => DataContext as BasePageViewModel;

		public BasePage(BasePageViewModel viewModel)
		{
		
			DataContext = viewModel;

			Loaded += Page_Loaded;
			Unloaded += Page_Unloaded;
		}

		protected virtual void Page_Loaded(object sender, RoutedEventArgs e)
		{			

			ViewModel.ModalWindowRequired += ViewModel_ModalWindowRequired;
			ViewModel.InfoDialogueRequired += ViewModel_InfoDialogueRequired;
			ViewModel.ConfirmDialogueRequired += ViewModel_ConfirmDialogueRequired;
			ViewModel.ErrorDialogueRequired += ViewModel_ErrorDialogueRequired;

			BackgroundTask.QueueTask(async (cancellationToken) =>
			{
				try
				{
					await ViewModel.OnPageLoadedAsync(cancellationToken);
					await ViewModel.LoadDataAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"ViewModel unload failed. Message={ex.Message}");
				}
			});
		}

		protected virtual void Page_Unloaded(object sender, RoutedEventArgs e)
		{


			Debug.Assert(ViewModel != null);

			ViewModel.ModalWindowRequired -= ViewModel_ModalWindowRequired;
			ViewModel.InfoDialogueRequired -= ViewModel_InfoDialogueRequired;
			ViewModel.ConfirmDialogueRequired -= ViewModel_ConfirmDialogueRequired;
			ViewModel.ErrorDialogueRequired -= ViewModel_ErrorDialogueRequired;

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
			var owner = Window.GetWindow(this);

			owner.IsEnabled = false;

			window.Owner = owner;
			window.Closed += (_, _) => owner.IsEnabled = true;
			window.Show();
		}

		private async void ViewModel_ConfirmDialogueRequired(object sender, ConfirmDialogModel model)
		{
			_contentDialog?.Hide();

			_contentDialog = new ContentDialog
			{
				Content = new TextBlock { Text = model.Message, FontSize = 24, TextWrapping = TextWrapping.Wrap },
				IsSecondaryButtonEnabled = false,
				IsPrimaryButtonEnabled = true,
				PrimaryButtonText = model.ApplyButtonText,
				PrimaryButtonCommand = new UICommand(model.ApplyCallback),
				CloseButtonText = model.CloseButtonText,
				DefaultButton = ContentDialogButton.Primary,
				Owner = Window.GetWindow(this)
			};
			_contentDialog.Closed += (s, e) => _contentDialog = null;

			if (model.CancelCallback != null)
				_contentDialog.CloseButtonCommand = new UICommand(model.CancelCallback);

			if (!string.IsNullOrEmpty(model.Title))
				_contentDialog.Title = new TextBlock { Text = model.Title, FontSize = 36, FontWeight = FontWeights.Bold };

			await _contentDialog.ShowAsync();
		}

		private async void ViewModel_InfoDialogueRequired(object sender, InfoDialogModel model)
		{
			_contentDialog?.Hide();

			_contentDialog = new ContentDialog
			{
				Content = new TextBlock { Text = model.Message, FontSize = 24, TextWrapping = TextWrapping.Wrap },
				IsSecondaryButtonEnabled = false,
				IsPrimaryButtonEnabled = false,
				CloseButtonText = model.CloseButtonText,
				DefaultButton = ContentDialogButton.None,
				Owner = Window.GetWindow(this)
			};
			_contentDialog.Closed += (s, e) => _contentDialog = null;

			if (!string.IsNullOrEmpty(model.Title))
				_contentDialog.Title = new TextBlock { Text = model.Title, FontSize = 36, FontWeight = FontWeights.Bold };

			await _contentDialog.ShowAsync();
		}

		private async void ViewModel_ErrorDialogueRequired(object sender, ErrorDialogModel model)
		{
			_contentDialog?.Hide();

			_contentDialog = new ContentDialog
			{
				Title = new TextBlock { Text = "Error", FontSize = 36, FontWeight = FontWeights.Bold },
				Content = new TextBlock { Text = model.Message, FontSize = 24, TextWrapping = TextWrapping.Wrap },
				IsSecondaryButtonEnabled = false,
				IsPrimaryButtonEnabled = false,
				CloseButtonText = model.CloseButtonText,
				DefaultButton = ContentDialogButton.None,
				Owner = Window.GetWindow(this)
			};
			_contentDialog.Closed += (s, e) => _contentDialog = null;

			if (model.ApplyCallback != null)
				_contentDialog.CloseButtonCommand = new UICommand(model.ApplyCallback);

			await _contentDialog.ShowAsync();
		}

	}
}
