using DB.configs;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DB
{
	public class SampleContext : DbContext
	{
		public SampleContext(DbContextOptions<SampleContext> options)
        : base(options)
		{
		}
 
		public SampleContext()
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SampleSecure;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration<User>(new UserConfig());
		}

		//entities
		public DbSet<User> Users { get; set; }

	}
}