using Domain.Customers.Queries;

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

		[Get("/api/Customer/GetCustomersCount")]
		Task<int> GetCustomersCount(string name, string companyName, string email, string phone);
	}
}
