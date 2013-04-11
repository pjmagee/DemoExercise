using FluentNHibernate.Mapping;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.Infrastructure.NHibernate.Config
{
    /// <summary>
    /// Fluent NHibernate configuration
    /// </summary>
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employee");

            Id(x => x.Id)
                .GeneratedBy.Guid()
                .Not.Nullable();

            Map(x => x.FirstName)
                .Length(32)
                .Not.Nullable();

            Map(x => x.LastName)
                .Length(32)
                .Not.Nullable();

            Map(x => x.DateOfBirth)
                .Not.Nullable();

            HasOne(x => x.Team)
                .Cascade.None();
        }
    }
}
