using DataAccess.Response;

using Domain.Customers.Commands;
using Domain.Customers.Queries;
using Domain.Interfaces;

using Entities;

using Refit;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Rest
{
	public interface ICustomerApi
	{	
		[Get("/api/Customer/GetCustomersPageQuery")]		
		Task<IEnumerable<Customer>> GetCustomersPageQuery([Query] GetCustomersPageQuery query);
		
		[Put("/api/Customer/Update")]
		Task<int> Update(int id, [Body] UpdateCustomerCommand customer);

		[Delete("/api/Customer/{id}")]		
		Task<int> DeleteCustomerCommand(int id);

		[Post("/api/Customer")]
		Task<Result<Customer>> CreateCustomer([Body] CreateCustomerCommand command);

		[Get("/api/Customer/GetCustomersCount")]
		Task<int> GetCustomersCount(string name, string companyName, string email, string phone);
	}
}
