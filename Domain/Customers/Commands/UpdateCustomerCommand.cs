using Domain.Interfaces;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Commands
{
	public record UpdateCustomerCommand(int CustomerId, string Name, string CompanyName, string Email, string Phone) : IRequest<int>;
	public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
	{
		private readonly ICustomersRepository repository;
		public UpdateCustomerCommandHandler(ICustomersRepository customerRepository)
		{
			repository = customerRepository;
		}
		public async Task<int> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
		{
			return await repository.UpdateCustomerAsync(command.CustomerId, command.Name, command.CompanyName, command.Email, command.Phone, cancellationToken);
		}
	}
}
