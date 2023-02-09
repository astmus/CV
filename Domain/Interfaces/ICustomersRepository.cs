using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ICustomersRepository
	{
		public Task<IRequestResult<Customer>> CreateCustomerAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default);
		public Task<int> GetCustomersCountAsync(string name, string companyName, string email, string phone, CancellationToken cancel = default);
		public Task<IEnumerable<Customer>> GetCustomersPageAsync(string name, string companyName, string email, string phone, int page, int pageCount, string sortBy, int sortDesc, CancellationToken cancel = default);
		public Task<int> DeleteCustomerAsync(int customerId, CancellationToken cancel = default);
		public Task<int> UpdateCustomerAsync(int customerId, string name, string companyName, string email, string phone, CancellationToken cancel = default);
	}
}
