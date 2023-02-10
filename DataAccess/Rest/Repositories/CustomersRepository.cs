using DataAccess.Response;
using DataAccess.Rest;

using Domain.Customers.Commands;
using Domain.Customers.Queries;
using Domain.Interfaces;

using Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataBase.Repositories
{
	internal class RestCustomersRepository : ICustomersRepository
	{
		ICustomerApi api;
		public RestCustomersRepository(ICustomerApi apiContext)
		{
			api = apiContext;
		}
		public async  Task<IRequestResult<Customer>> CreateCustomerAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{
			
			var result = await api.CreateCustomer(new CreateCustomerCommand(name, companyName, email, phone));
			return result;

		}

		public async Task<int> DeleteCustomerAsync(int customerId, CancellationToken cancel = default)
		{			
			var deleted = await api.DeleteCustomerCommand(customerId);
			return deleted;
		}

		public async Task<int> GetCustomersCountAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{
			var count = await api.GetCustomersCount(name, companyName, email, phone);
			return count; 
		}
		
		public async Task<IEnumerable<Customer>> GetCustomersPageAsync(GetCustomersPageQuery query, CancellationToken cancel = default)
		{
			var result = await api.GetCustomersPageQuery(query);
			return result;
		}		

		public async Task<int> UpdateCustomerAsync(int customerId , string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{			
			var result = await api.Update(customerId, new UpdateCustomerCommand(customerId, name, companyName, email, phone));
			return result;
		}
	}
}
