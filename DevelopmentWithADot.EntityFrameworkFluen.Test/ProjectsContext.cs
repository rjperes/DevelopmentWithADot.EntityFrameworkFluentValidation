using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;

namespace DevelopmentWithADot.EntityFrameworkFluen.Test
{
	public class ProjectsContext : DbContext
	{
		static ProjectsContext()
		{
			Database.SetInitializer<ProjectsContext>(null);
		}

		public ProjectsContext() : base("Name=ProjectsContext")
		{
		}
		
		public DbSet<Resource> Resources
		{
			get;
			set;
		}

		public DbSet<Project> Projects
		{
			get;
			set;
		}

		public DbSet<Customer> Customers
		{
			get;
			set;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<Project>().HasOptional(x => x.Detail).WithRequired(x => x.Project).WillCascadeOnDelete(true);

			base.OnModelCreating(modelBuilder);
		}
	}
}
