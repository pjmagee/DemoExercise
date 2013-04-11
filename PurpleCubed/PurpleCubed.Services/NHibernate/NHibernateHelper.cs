using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace PurpleCubed.DataAccess.NHibernate
{
    public class NHibernateHelper
    {
        private readonly string connectionString;
        private ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory
        {
            get { return sessionFactory ?? (sessionFactory = CreateSessionFactory()); }
        }

        public NHibernateHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
    }
}