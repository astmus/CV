using Domain.Interfaces;

using Entities;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfClient.Commands;
using WpfClient.Models;
using WpfClient.Pages;

namespace WpfClient.ViewModels
{
	public class EditPageViewModel : BasePageViewModel
	{
		public AsyncUICommand<Customer> SubmitCommand { get => Get<AsyncUICommand<Customer>>(); protected set => Set(value); }
		public ICommand CancelCommand { get; }
		public Customer CustomerData { get => Get<Customer>(); set => Set(value); }
		ICustomersRepository repository;
		IPageNavigateProvider navigate;
		public EditPageViewModel(ICustomersRepository customerRepository, IPageNavigateProvider navigation)
		{
			repository = customerRepository;
			navigate = navigation;
			SubmitCommand = new AsyncUICommand<Customer>(UpdateAsync, IsCanExecute, OnCommandException);
			CancelCommand = new UICommand(Cancel);			
		}

		private void Cancel()
		{
			navigate.NavigateToPage<ViewPage>();
		}
		private async Task CreateCustomerAsync(Customer customer)
		{
			try
			{
				IsInProgress = true;
				var result = await repository.CreateCustomerAsync(customer.Name, customer.CompanyName, customer.Email, customer.Phone);
				if (result.Success)
					navigate.NavigateToPage<ViewPage>();					
				else
					RaiseErrorDialogRequired(new ErrorDialogModel() { Title = "Error", Message = result.Message, CloseButtonText = "ok" });
			}
			catch (Exception ex)
			{
				OnCommandException(ex);
			}
			finally { IsInProgress = false; }
		}	

		private async Task UpdateAsync(Customer customer)
		{
			try
			{
				IsInProgress = true;
				var result = await repository.UpdateCustomerAsync(customer.CustomerId, customer.Name, customer.CompanyName, customer.Email, customer.Phone);				
				navigate.NavigateToPage<ViewPage>();
			}
			catch (Exception ex)
			{
				OnCommandException(ex);
			}
			finally { IsInProgress = false; }
		}

		public override async Task OnPageLoadedAsync(CancellationToken cancellationToken)
		{
			await base.OnPageLoadedAsync(cancellationToken);
			if (this[nameof(Customer)] is Customer customer)
				CustomerData = new Customer() { Name = customer.Name, CompanyName = customer.CompanyName, CustomerId = customer.CustomerId, Email = customer.Email, Phone = customer.Phone };
			else
			{
				SubmitCommand = new AsyncUICommand<Customer>(CreateCustomerAsync, IsCanExecute, OnCommandException);
				CustomerData = new Customer();
			}
		}
	}
}
