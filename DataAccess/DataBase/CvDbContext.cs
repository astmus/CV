using Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DataBase
{
	public class CvDbContext : DbContext
	{

		public CvDbContext()
		{

		}
		public CvDbContext(DbContextOptions<CvDbContext> options) : base(options)
		{

		}
		public DbSet<Customer> Customers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var conf = BuildConfiguration();
			optionsBuilder.UseSqlServer(conf.GetConnectionString("Default"));
		}

		private static IConfigurationRoot BuildConfiguration()
		{
			var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false)
			.AddJsonFile("appsettings.Development.json");
			//.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DataAccess/DataBase/Migrations/"));

			return builder.Build();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.HasKey(e => e.CustomerId);
				entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
				entity.HasIndex(nameof(Customer.CompanyName));
				entity.HasIndex(nameof(Customer.Email));
				entity.HasIndex(nameof(Customer.Name));
				entity.HasIndex(nameof(Customer.Phone));
				entity.ToTable($"{nameof(Customer)}s");
			});

			modelBuilder.Entity<Order>(e =>
			{
				e.HasKey(k => k.CustomerId);
				e.Property(p => p.Name).HasColumnType("varchar(50)");
				e.Property(p => p.Price).HasColumnType("decimal(8,2)");
			});
		}
	}
}
