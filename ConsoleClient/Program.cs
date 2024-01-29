using DataAccess.DataBase;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var builder = Host.CreateApplicationBuilder();
			builder.Configuration.AddJsonFile("appsettings.Development.json");
			builder.Services.AddDbContext<CvDbContext>((sp, op) 
				=> { op.UseSqlServer(builder.Configuration.GetConnectionString("Default")); });
			var host = builder.Build();
			var db = host.Services.GetRequiredService<CvDbContext>();
			db.Database.Migrate();			
			Console.ReadKey();
			db.Customers.Add(new Entities.Customer() { CompanyName = "cm" });
			var cnt =  db.SaveChanges();
		}
	}
}
