using DataAccess.Response;
using DataAccess.Rest;

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

			//var customer = await api.Customers.Where(a => a.Name == name).FirstOrDefaultAsync();
			//if (customer == null)
			//{
			//	var result = await api.Customers.AddAsync(new Customer() { Name = name, CompanyName = companyName, Email = email, Phone = phone }, cancel);
			//	await api.SaveChangesAsync(cancel);
			//	return new Result<Customer>(result.Entity);
			//}
			//else
				return await Task.FromResult(new Result<Customer>(Success: false, Message: $"Customer with name {name} already exist"));

		}

		public Task<int> DeleteCustomerAsync(int customerId, CancellationToken cancel = default)
		{
			//var customer = await api.Customers.Where(a => a.CustomerId == customerId).FirstOrDefaultAsync();
			//if (customer == null) return default;
			//api.Customers.Remove(customer);
			//await api.SaveChangesAsync();
			return Task.FromResult(customerId);
		}

		public async Task<int> GetCustomersCountAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{
			var count = await api.GetCustomersCount(name, companyName, email, phone);
			return count; 
		}

		
		public async Task<IEnumerable<Customer>> GetCustomersPageAsync(GetCustomersPageQuery query, CancellationToken cancel = default)
		{
			var result = await api.GetCustomersPageQuery(query);// .Customers.FromSqlInterpolated($"FindCustomers {name ?? ""}, {companyName ?? ""}, {email ?? ""}, {phone ?? ""}, {page}, {pageCount}, {sortBy ?? ""}, {sortDesc}").ToListAsync(cancel);
			return result;
		}

		public Task<IEnumerable<Customer>> GetCustomersPageAsync(string name, string companyName, string email, string phone, int page, int pageCount, string sortBy, int sortDesc, CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}

		public Task<int> UpdateCustomerAsync(int customerId , string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{
			//var customer = await api.Customers.Where(a => a.CustomerId == customerId).FirstOrDefaultAsync();
			//if (customer == null) return default;
			//customer.Name = name;
			//customer.CompanyName = companyName;
			//customer.Email = email;
			//customer.Phone = phone;
			//await api.SaveChangesAsync();
			return Task.FromResult(customerId);
		}
	}
}
