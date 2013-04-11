using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.Infrastructure.EntityFramework.Config
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee");
            HasKey(x => x.Id);
            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.DateOfBirth).IsOptional();
            Property(x => x.TeamId).IsOptional();
            HasOptional(x => x.Team).WithMany(x => x.Employees).HasForeignKey(x => x.TeamId);
        }
    }
}
