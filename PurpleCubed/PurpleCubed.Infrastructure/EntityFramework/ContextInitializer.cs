using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.Infrastructure.EntityFramework
{
    // This is the initializer for the Database using Entity Framework
    // I love to use this for test object generation including 
    // other frameworks such as Faker.Net for real visual looking data
    // but for the simplicity of this application i only used NBuilder (which is amazing)
    public class ContextInitializer : DropCreateDatabaseAlways<CubedContext>
    {
        protected override void Seed(CubedContext ctx)
        {
            // http://nbuilder.org/Documentation/Lists

            var employees = Builder<Employee>.CreateListOfSize(50)
                .All()
                .Do(x => x.TeamId = null) // We arent specifying any teams
                .Do(x => x.FirstName = Faker.NameFaker.FirstName())
                .Do(x => x.LastName = Faker.NameFaker.LastName())
                .Do(x => x.DateOfBirth = Faker.DateTimeFaker.BirthDay())
                .Do(x=> x.Email = Faker.InternetFaker.Email())
                .Build()
                .ToList(); // expose iterator


            var teams = Builder<Team>.CreateListOfSize(20)
                .All()
                .Do(x=> x.Name = Faker.CompanyFaker.Name())
                .Do(x => x.Employees = new Collection<Employee>())
                .Do(x => x.DateCreated = DateTime.Now)
                .Build()
                .ToList(); // expose  iterator

            employees.ForEach(x => ctx.Employees.Add(x)); // Add the employees to EF context
            teams.ForEach(x => ctx.Teams.Add(x)); // Add the teams to the EF context

            ctx.SaveChanges();

        }
    }
}