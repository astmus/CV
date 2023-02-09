﻿using Domain.Interfaces;

using Entities;

using MediatR;

namespace Domain.Customers.Queries
{
	public record GetCustomersPageQuery(string Name, string CompanyName, string Email, string Phone, int Page, int PageCount, string SortBy, int SortDesc) : IRequest<IEnumerable<Customer>>;
	public class GetCustomersPageQueryHandler : IRequestHandler<GetCustomersPageQuery, IEnumerable<Customer>>
	{
		private readonly ICustomersRepository repository;
		public GetCustomersPageQueryHandler(ICustomersRepository customerRepository)
		{
			repository = customerRepository;
		}
		public async Task<IEnumerable<Customer>> Handle(GetCustomersPageQuery query, CancellationToken cancellationToken)
		{
			return await repository.GetCustomersPageAsync(query.Name, query.CompanyName, query.Email, query.Phone,query.Page,query.PageCount,query.SortBy,query.SortDesc, cancellationToken);
		}
	}
}
