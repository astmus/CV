using DataAccess.Response;

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
	internal class DbCustomersRepository : ICustomersRepository
	{
		CvDbContext context;
		public DbCustomersRepository(CvDbContext dbContext)
		{
			context = dbContext;
		}
		public async Task<IRequestResult<Customer>> CreateCustomerAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{

			var customer = await context.Customers.Where(a => a.Name == name).FirstOrDefaultAsync();
			if (customer == null)
			{
				var result = await context.Customers.AddAsync(new Customer() { Name = name, CompanyName = companyName, Email = email, Phone = phone }, cancel);
				await context.SaveChangesAsync(cancel);
				return new Result<Customer>(result.Entity);
			}
			else
				return new Result<Customer>(Success: false, Message: $"Customer with name {name} already exist");

		}

		public async Task<int> DeleteCustomerAsync(int customerId, CancellationToken cancel = default)
		{
			var customer = await context.Customers.Where(a => a.CustomerId == customerId).FirstOrDefaultAsync();
			if (customer == null) return default;
			context.Customers.Remove(customer);
			await context.SaveChangesAsync();
			return customer.CustomerId;
		}

		public async Task<int> GetCustomersCountAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{
			int result;
			using (System.Data.Common.DbCommand command = context.Database.GetDbConnection().CreateCommand())
			{
				
				command.CommandText = $"CountCustomers '{name}', '{companyName}', '{email}', '{phone}'";
				await context.Database.OpenConnectionAsync();

				result = (int?)(await command.ExecuteScalarAsync()) ?? 0;

				await context.Database.CloseConnectionAsync();
			}			
			return result;
		}
		
		public async Task<IEnumerable<Customer>> GetCustomersPageAsync(GetCustomersPageQuery query, CancellationToken cancel = default)
		{
			var result = await context.Customers.FromSqlInterpolated($"FindCustomers {query.Name ?? ""}, {query.CompanyName ?? ""}, {query.Email ?? ""}, {query.Phone ?? ""}, {query.Page}, {query.PageCount}, {query.SortBy ?? ""}, {query.SortDesc}").ToListAsync(cancel);
			return result;
		}

		public async Task<int> UpdateCustomerAsync(int customerId , string name, string companyName, string email, string phone, CancellationToken cancel = default)
		{
			var customer = await context.Customers.Where(a => a.CustomerId == customerId).FirstOrDefaultAsync();
			if (customer == null) return default;
			customer.Name = name;
			customer.CompanyName = companyName;
			customer.Email = email;
			customer.Phone = phone;
			await context.SaveChangesAsync();
			return customer.CustomerId;
		}
	}
}
