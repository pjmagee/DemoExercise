using System.Data.Entity.ModelConfiguration;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.Infrastructure.EntityFramework.Config
{
    public class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            ToTable("Team");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(32).IsRequired();
            HasMany(x => x.Employees).WithOptional(x => x.Team).HasForeignKey(x => x.TeamId).WillCascadeOnDelete(value: false);
        }
    }
}