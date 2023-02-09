using Domain.Data;
using Domain.Interfaces;

using Entities;

using MediatR;

namespace Domain.Customers.Queries
{
	public record GetCustomersCountQuery(string Name, string CompanyName, string Email, string Phone) : IRequest<int>;
	public class GetCustomersCountQueryHandler : IRequestHandler<GetCustomersCountQuery, int>
	{
		private readonly ICustomersRepository repository;
		public GetCustomersCountQueryHandler(ICustomersRepository customerRepository)
		{
			repository = customerRepository;
		}
		public async Task<int> Handle(GetCustomersCountQuery command, CancellationToken cancellationToken)
		{
			return await repository.GetCustomersCountAsync(command.Name, command.CompanyName, command.Email, command.Phone, cancellationToken);
		}
	}
}
