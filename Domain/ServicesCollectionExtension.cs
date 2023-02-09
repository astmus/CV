using Domain.Interfaces;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Domain
{
	public static class ServicesCollectionExtension
	{
		public static void AddDomainCore(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
		}
	}
}
