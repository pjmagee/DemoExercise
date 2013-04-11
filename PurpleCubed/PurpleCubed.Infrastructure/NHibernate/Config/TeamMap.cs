using FluentNHibernate.Mapping;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.Infrastructure.NHibernate.Config
{
    /// <summary>
    /// Fluent NHibernate configuration
    /// </summary>
    public class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            Table("Team");

            Id(x => x.Id)
                .GeneratedBy.Guid()
                .Not.Nullable();

            Map(x => x.DateCreated)
                .Not.Nullable();

            Map(x => x.Name)
                .Length(32)
                .Not.Nullable();
        }
    }
}