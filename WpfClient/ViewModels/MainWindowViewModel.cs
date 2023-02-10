using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

using WpfClient.Commands;
using WpfClient.Pages;

namespace WpfClient.ViewModels
{
	public class MainWindowViewModel : BasePageViewModel
	{
		#region Fields
		
		private readonly IPageNavigateProvider pageNavigator;
		private readonly ICustomersRepository repository;

		#endregion

		#region Properties
		protected override bool IsCanExecute() => !(IsInProgress || PageViewModel == null || PageViewModel?.IsInProgress == true);

		protected override bool IsCanExecute(object obj) => !(IsInProgress || PageViewModel == null || PageViewModel?.IsInProgress == true);

		public BasePageViewModel PageViewModel { get => Get<BasePageViewModel>(); set => Set(value); }


		#endregion


		public MainWindowViewModel(IPageNavigateProvider pageProvider, ICustomersRepository qustomers)
		{	
			pageNavigator = pageProvider;
			repository = qustomers;
			PageViewModel = this;
		}

		public override async Task LoadDataAsync(CancellationToken cancellationToken)
		{
			await base.LoadDataAsync(cancellationToken);

			try
			{
				IsInProgress = true;

				int totalCount = await repository.GetCustomersCountAsync(null, null, null, null, cancellationToken);

				IsInProgress = false;

				ViewPage page = pageNavigator.NavigateToPage<ViewPage>();
				page.ViewModel[nameof(totalCount)] = totalCount;
			}
			catch (Exception ex)
			{
				OnCommandException(ex);	
			}
			finally { IsInProgress = false; }
		}
		
	}
}
