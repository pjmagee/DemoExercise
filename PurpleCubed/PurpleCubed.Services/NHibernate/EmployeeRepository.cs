using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Ninject;
using PurpleCubed.Domain;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.DataAccess.NHibernate
{
    /// <summary>
    ///  Something like this could be adopted
    /// 
    /// http://blog.bobcravens.com/2010/07/using-nhibernate-in-asp-net-mvc/
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        // Maintain Hibernate Session through Session Factory
        // Could use something like Ninject Provider for SessionFactory which then returns session
        private readonly ISession session;

        [Inject]
        public EmployeeRepository(ISession session)
        {
            if (session == null) 
                throw new ArgumentNullException("session");

                this.session = session;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = session.CreateCriteria<Employee>().List<Employee>();
            return employees;
        }

        public Employee GetById(int id)
        {
            var employee = session.Get<Employee>(id);
            return employee;
        }

        public Employee Create(Employee entity)
        {
            var employeeId = session.Save(entity);
            Employee employee = session.Get<Employee>(employeeId);
            return employee;
        }

        public void Delete(int id)
        {
            var employee = session.Get<Employee>(id);
            session.Delete(employee);
        }

        public Employee Update(Employee entity)
        {
            session.SaveOrUpdate(entity);
            var employee = session.Get<Employee>(entity.Id);
            return employee;
        }
    }
}
