using System.Configuration;
using NHibernate;
using NHibernate.Cfg;
using Ninject.Modules;
using Ninject.Web.Common;
using PurpleCubed.Domain;

namespace PurpleCubed.DataAccess.NHibernate
{
    public class NHibernateModule : NinjectModule
    {
        public override void Load()
        {
            NHibernateHelper helper = new NHibernateHelper(ConfigurationManager.ConnectionStrings["NHibernateConnection"].ConnectionString);

            Bind<ISessionFactory>().ToConstant(helper.SessionFactory).InSingletonScope();

            Bind<ISession>()
               .ToProvider<SessionProvider>() // From Session Factory in Session Provider
               .InRequestScope(); // Inject instance per HTTP Request

            Bind<ITeamRepository>().To<TeamRepository>();

            Bind<IEmployeeRepository>()
                .To<EmployeeRepository>()
                .InRequestScope(); 
        }
    }
}
