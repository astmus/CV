using Domain.Interfaces;

using Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Commands
{
	public record CreateCustomerCommand(string Name, string CompanyName, string Email, string Phone) : IRequest<IRequestResult<Customer>>;
	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IRequestResult<Customer>>
	{
		private readonly ICustomersRepository repository;
		public CreateCustomerCommandHandler(ICustomersRepository customerRepository)
		{
			repository = customerRepository;
		}
		public async Task<IRequestResult<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
		{
			return await repository.CreateCustomerAsync(command.Name, command.CompanyName, command.Email, command.Phone, cancellationToken);			
		}
	}
}
