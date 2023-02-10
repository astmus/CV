using Domain.Customers.Queries;
using Domain.Interfaces;

using Entities;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfClient.Commands;
using WpfClient.Models;
using WpfClient.Pages;

namespace WpfClient.ViewModels
{
	public class ViewPageViewModel : BasePageViewModel
	{
		public ICommand LoadCommand { get; }
		public ICommand NextPageCommand { get; }
		public ICommand PrevPageCommand { get; }
		public ICommand DeleteCustomerCommand { get; }
		public ICommand CreateCustomerCommand { get; }
		public ICommand EditCustomerCommand { get; }
		public string Name { get => Get<string>(); set => Set(value); }
		public string CompanyName { get => Get<string>(); set => Set(value); }
		public string Email { get => Get<string>(); set => Set(value); }
		public string Phone { get => Get<string>(); set => Set(value); }
		public int PageItemsCount { get => Get<int>(10); set => Set(value); }
		public string SortBy { get => Get<string>(); set => Set(value); }
		public bool SortDesc { get => Get<bool>(); set => Set(value); }
		public ObservableCollection<Customer> Customers { get => Get<ObservableCollection<Customer>>(); set => Set(value); }
		public PaginationInfo PageInfo { get => Get<PaginationInfo>(); set => Set(value); }
		public Customer CurrentSelected { get => Get<Customer>(); set => Set(value); }

		ICustomersRepository repository;
		IPageNavigateProvider navigation;
		public ViewPageViewModel(ICustomersRepository customerRepository, IPageNavigateProvider provider)
		{
			navigation = provider;
			LoadCommand = new AsyncUICommand(LoadCustomers, IsCanExecute, OnCommandException);
			NextPageCommand = new AsyncUICommand<PaginationInfo>(NextPageCommandAsync, IsCanExecute, OnCommandException);
			PrevPageCommand = new AsyncUICommand<PaginationInfo>(PrevPageCommandAsync, IsCanExecute, OnCommandException);
			DeleteCustomerCommand = new AsyncUICommand<Customer>(DeleteCustomerAsync, IsCanExecute, OnCommandException);
			CreateCustomerCommand = new UICommand<Customer>(CreateCustomer);
			EditCustomerCommand = new UICommand<Customer>(EditCustomer);
			Customers = new ObservableCollection<Customer>();
			repository = customerRepository;
		}

		private void CreateCustomer(Customer customer)
		{
			try
			{
				IsInProgress = true;
				var page = navigation.NavigateToPage<EditPage>();
			}
			catch (Exception ex)
			{
				OnCommandException(ex);
			}
			finally { IsInProgress = false; }
		}

		private void EditCustomer(Customer customer)
		{
			var page = navigation.NavigateToPage<EditPage>();
			page.ViewModel[nameof(Customer)] = customer;
			
		}

		private async Task DeleteCustomerAsync(Customer customer)
		{
			try
			{
				IsInProgress = true;
				await repository.DeleteCustomerAsync(customer.CustomerId);
				Customers.Remove(customer);
			}
			catch (Exception ex)
			{
				OnCommandException(ex);				
			}
			finally { IsInProgress = false; }

		}

		async Task LoadCustomers()
		{
			try
			{
				IsInProgress = true;
				Customers.Clear();

				int totalCount = await repository.GetCustomersCountAsync(Name, CompanyName, Email, Phone);
				PageInfo = new PaginationInfo()
				{
					ItemsCount = totalCount,
					ItemsPerPage = PageItemsCount,
					PagesCount = totalCount / PageItemsCount,
					PageNumber = PageInfo.PageNumber < totalCount / PageItemsCount ? PageInfo.PageNumber : 0
				};

				var customers = await repository.GetCustomersPageAsync(new GetCustomersPageQuery(Name, CompanyName, Email, Phone, PageInfo.PageNumber, PageItemsCount, SortBy, SortDesc ? 1 : 0));
				customers.ToList().ForEach(c => Customers.Add(c));
			}
			catch (Exception ex)
			{
				OnCommandException(ex);
			}
			finally { IsInProgress = false; }
		}

		async Task NextPageCommandAsync(PaginationInfo info)
		{
			if (PageInfo.PageNumber < PageInfo.PagesCount)
			{
				PageInfo.PageNumber++;
				await LoadCustomers();
			}
		}
		async Task PrevPageCommandAsync(PaginationInfo info)
		{
			if (PageInfo.PageNumber > 0)
			{
				PageInfo.PageNumber--;
				await LoadCustomers();
			}
		}

		public override async Task LoadDataAsync(CancellationToken cancellationToken)
		{
			try
			{
				IsInProgress = true;
				Customers.Clear();
				var totalCount = (int?)this["totalCount"] ?? await repository.GetCustomersCountAsync(Name, CompanyName, Email, Phone);
				PageInfo ??= new PaginationInfo() { ItemsCount = totalCount, ItemsPerPage = PageItemsCount, PagesCount = totalCount / PageItemsCount, PageNumber = 0 };
				var customers = await repository.GetCustomersPageAsync(new GetCustomersPageQuery(Name, CompanyName, Email, Phone, PageInfo.PageNumber, PageItemsCount, null, 0));
				customers.ToList().ForEach(c => Customers.Add(c));				
			}
			catch (Exception ex)
			{
				OnCommandException(ex);				
			}
			finally { IsInProgress = false; }
		}
	}
}
