using Domain.Interfaces;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Commands
{
	public record DeleteCustomerCommand(int CustromerId) : IRequest<int>;
	public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
	{
		private readonly ICustomersRepository repository;
		public DeleteCustomerCommandHandler(ICustomersRepository customerRepository)
		{
			repository = customerRepository;
		}
		public async Task<int> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
		{
			return await repository.DeleteCustomerAsync(command.CustromerId, cancellationToken);
		}
	}
}
