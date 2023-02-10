using DataAccess.DataBase;
using DataAccess.DataBase.Repositories;
using DataAccess.Rest;

using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Refit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public static class ServicesCollectionExtension
	{
		public static void AddDbDataAccess(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<CvDbContext>(options =>
				options.UseSqlServer(
					connectionString,
					b => b.MigrationsAssembly(typeof(CvDbContext).Assembly.FullName)));
			services.AddScoped<ICustomersRepository, DbCustomersRepository>();
		}
		public static IServiceCollection AddRestDataAccess(this IServiceCollection services, string apiAddress)
		{
			services.AddRefitClient<ICustomerApi>(RefitSettings).ConfigureHttpClient(client=> client.BaseAddress = new Uri(apiAddress));
			services.AddTransient<ICustomersRepository, RestCustomersRepository>();
			return services;
		}
		static readonly RefitSettings RefitSettings = new RefitSettings()
		{
			Buffered = true,
			ContentSerializer = new NewtonsoftJsonContentSerializer(
					new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore,
						ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
						MissingMemberHandling = MissingMemberHandling.Ignore
					})
		};
	}
}
