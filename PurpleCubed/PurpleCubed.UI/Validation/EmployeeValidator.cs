using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Ninject;
using PurpleCubed.Domain;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.UI.Validation
{
    /// <summary>
    /// I tend to prefer fluent validation 
    /// Similar to Fluent Configurations like Fluent NHibernate / Entity Framework FluentAPI 
    /// I believe it looks much cleaner than having to use DataAnnotations from the Component Model Namespace
    /// It comes with a lot of custom validation rules which can be plugged right into ASP.NET MVC with some
    /// ninject for Model Validation Provider
    /// 
    /// Also FluentValidation was originally created by Jeremy Skinner, a developer living just outside
    /// of London in Hertfordshire, Watford area as a .NET Developer and co-author of 'ASP.NET MVC 4 in Action'
    /// </summary>
    public class EmployeeValidator : AbstractValidator<Employee>
    {

        [Inject] // Very bad code design for Property Injection - ;-) Just letting you know I KNOW this is not good.
        public IEmployeeRepository EmployeeRepository { get; set; }
 
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEqual(x => x.LastName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth);
            RuleFor(x => x.Email).EmailAddress().Must(BeUnique).WithMessage("That email has been taken.").NotNull();
        }

        private bool BeUnique(Employee e, string s)
        {
            return !EmployeeRepository.GetAll()
                .Where(x => x.Id != e.Id)
                .Any(x => x.Email.Equals(s, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}