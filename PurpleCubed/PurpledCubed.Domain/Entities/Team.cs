using System;
using System.Collections.Generic;

namespace PurpleCubed.Domain.Entities
{
    /// <summary>
    /// Plain old CLR object
    /// </summary>
    public class Team
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}