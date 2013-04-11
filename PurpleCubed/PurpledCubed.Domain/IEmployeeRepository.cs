using PurpleCubed.Domain.Entities;

namespace PurpleCubed.Domain
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Any unique repository requirements
    }
}