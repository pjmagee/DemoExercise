using System;
using NHibernate;
using Ninject;
using Ninject.Activation;

namespace PurpleCubed.DataAccess.NHibernate
{
    public class SessionProvider : Provider<ISession>
    {
        protected override ISession CreateInstance(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();
            var session = sessionFactory.OpenSession();
            session.BeginTransaction();
            return session;
        }
    }
}