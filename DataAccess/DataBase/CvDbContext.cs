using Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			optionsBuilder.UseSqlServer();
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
		}
	}
}
