using System;

namespace PurpleCubed.Domain.Entities
{
    /// <summary>
    /// Plain old CLR object
    /// </summary>
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual Team Team { get; set; }
        public virtual int? TeamId { get; set; }
    }
}
