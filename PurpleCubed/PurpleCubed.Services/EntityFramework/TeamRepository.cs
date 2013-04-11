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
    public class TeamRepository : ITeamRepository
    {
        private readonly CubedContext context;

        [Inject]
        public TeamRepository(CubedContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public IEnumerable<Team> GetAll()
        {
            return context.Teams.ToList();
        }

        public Team GetById(int id)
        {
            var team = context.Teams.Find(id);
            return team;
        }

        public Team Create(Team entity)
        {
            var team = context.Teams.Add(entity);
            context.SaveChanges();
            return team;
        }

        public void Delete(int id)
        {
            var team = context.Teams.Find(id);
            context.Teams.Remove(team);
            context.SaveChanges();
        }

        public Team Update(Team entity)
        {
            Team team = context.Teams.Find(entity.Id);
            context.Entry(team).CurrentValues.SetValues(entity);
            context.Entry(team).State = EntityState.Modified;
            context.SaveChanges();
            return team;
        }
    }


}
