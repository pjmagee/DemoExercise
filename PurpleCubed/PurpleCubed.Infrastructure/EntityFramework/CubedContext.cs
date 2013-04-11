using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PurpleCubed.Domain.Entities;
using PurpleCubed.Infrastructure.EntityFramework.Config;

namespace PurpleCubed.Infrastructure.EntityFramework
{
    public class CubedContext : DbContext
    {
        public DbSet<Team> Teams { get; set; } // Could use IDbSet for Mocking, something I want to explore
        public DbSet<Employee> Employees { get; set; } // Could use IDbset for Mocking, something I want to explore
 
        public CubedContext() : base(nameOrConnectionString: "DefaultConnection") // from Web.Config XML
        {
            Configuration.ValidateOnSaveEnabled = true; // If you wanted Data Integrity, but hopefully a service layer and UI validation is enough
            Configuration.ProxyCreationEnabled = true; // Enables change tracking including Lazy Loading via proxy classes
            Configuration.LazyLoadingEnabled = true; // Lazy loading of collections can improve performance depending on the scenario
            Configuration.AutoDetectChangesEnabled = true; // I always have this set because of multiple assocations, something might change on either side of an assocation
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        // Configuration for Entity Framework
        private void ConfigureModel(DbModelBuilder modelBuilder)
        {
            // remove these conventions, I like security before cascading important information
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Similar to NHibernate Fluent Config
            modelBuilder.Configurations
                        .Add(new TeamConfiguration())
                        .Add(new EmployeeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
