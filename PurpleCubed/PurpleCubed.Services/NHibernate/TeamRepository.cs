using System;
using System.Collections.Generic;
using NHibernate;
using Ninject;
using PurpleCubed.Domain;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.DataAccess.NHibernate
{
    public class TeamRepository : ITeamRepository
    {
        // Maintain Hibernate Session through Session Factory
        // Could use something like Ninject Provider for SessionFactory 
        // which then returns the actual session
        private readonly ISession session;

        [Inject]
        public TeamRepository(ISession session)
        {
            if (session == null) 
                throw new ArgumentNullException("session");

                this.session = session;
        }

        public IEnumerable<Team> GetAll()
        {
            var teams = session.CreateCriteria<Team>().List<Team>();
            return teams;
        }

        public Team GetById(int id)
        {
            var team = session.Get<Team>(id);
            return team;
        }

        public Team Create(Team entity)
        {
            var id = session.Save(entity);
            Team team = session.Get<Team>(id);
            return team;
        }

        public void Delete(int id)
        {
            session.Delete(id);
        }

        public Team Update(Team entity)
        {
            session.SaveOrUpdate(entity);
            Team team = session.Get<Team>(entity.Id);
            return team;
        }
    }
}
