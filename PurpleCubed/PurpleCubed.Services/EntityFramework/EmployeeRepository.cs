using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ninject;
using PurpleCubed.Domain;
using PurpleCubed.Domain.Entities;
using PurpleCubed.Infrastructure.EntityFramework;

namespace PurpleCubed.DataAccess.EntityFramework
{
    /// <summary>
    /// The Entity Framework Repository Implementation 
    /// To be injected into our UI Controller 
    /// 
    /// Must ensure that the Context is injected per HTTP Request
    /// So that if multiple services were used that all consumed 
    /// this repository in one request the Context would keep track 
    /// of all these changes without causing issues. (Rather than injecting new instances of the Context into the repository)
    /// 
    /// Weakness: I'm not too clued up on all the Repository and Unit Of Work Patterns
    /// Some consider the Repository Pattern an Anti-Pattern, Some do not. 
    /// 
    /// I don't have enough experience to provide a full argument for any of it.
    /// 
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CubedContext context;

        [Inject]
        public EmployeeRepository(CubedContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return context.Employees.Find(id);
        }

        public Employee Create(Employee entity)
        {
            Employee employee = context.Employees.Add(entity);
            context.SaveChanges();
            return employee;
        }

        public void Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            context.Employees.Remove(employee);
            context.SaveChanges();
        }

        public Employee Update(Employee entity)
        {
            Employee employee = context.Employees.Find(entity.Id);
            context.Entry(employee).CurrentValues.SetValues(entity);
            context.Entry(employee).State = EntityState.Modified;
            context.SaveChanges();

            return employee;
        }
    }
}