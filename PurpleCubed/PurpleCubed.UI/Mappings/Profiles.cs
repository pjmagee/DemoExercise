using AutoMapper;
using PurpleCubed.Domain.Entities;
using PurpleCubed.UI.Models;

namespace PurpleCubed.UI.Mappings
{
    /// <summary>
    /// The Team Profile is a specific profile for mapping
    /// between a true team entity object and a view model
    /// representation of that entity
    /// </summary>
    public class TeamProfile : Profile
    {
        protected override void Configure()
        {
            // Source --> Destination
            Mapper.CreateMap<Team, TeamSummaryViewModel>()
                  .ForMember(viewModel => viewModel.NumberOfEmployees, options => options.MapFrom(team => team.Employees.Count));
            
            // Mapping for Id, FirstName, LastName, DateCreated auto AUTOMATICALLY mapped due to naming convention being the same
            // in both the entity class and the view model class properties

        }
    }

    /// <summary>
    /// The User Profile is a specific Profile for mapping
    /// between a true user type entity, for example
    /// an Employee entity, an Admin entity, or other domain entities
    /// that are to be added of type User 
    /// (If the application was to mature, something similar to these would be expected) ?
    /// </summary>
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            // Source --> Destination
            Mapper.CreateMap<Employee, EmployeeViewModel>()
                .ForMember(x => x.FullName, opt => opt.ResolveUsing<FullNameResolver>()); // Example of ViewModel specific property mappings
           
            Mapper.CreateMap<EmployeeViewModel, Employee>();

            // These can reduce large views containing logic which should NOT be handled by the view for well-organised, structured applications
            // These sort of things should be handled by a Mapper, so that the view is given strongly typed view models
            // And does not have to handle things such as First Name + Last Name, etc etc
            // Resolvers can be used for large collections of entities, conversions between datetime objects to strings
            // Currency conversions etc
        }

        public class FullNameResolver : ValueResolver<Employee, string>
        {
            protected override string ResolveCore(Employee source)
            {
                return (source.FirstName + " " + source.LastName);
            }
        }

    }
}