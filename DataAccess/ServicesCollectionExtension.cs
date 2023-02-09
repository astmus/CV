using DataAccess.DataBase;
using DataAccess.DataBase.Repositories;

using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
			services.AddScoped<ICustomersRepository, CustomersRepository>();
		}
	}
}
