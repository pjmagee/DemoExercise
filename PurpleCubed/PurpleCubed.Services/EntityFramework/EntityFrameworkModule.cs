using Ninject.Modules;
using Ninject.Web.Common;
using PurpleCubed.Domain;
using PurpleCubed.Infrastructure.EntityFramework;

namespace PurpleCubed.DataAccess.EntityFramework
{
    public class EntityFrameworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITeamRepository>().To<TeamRepository>().InRequestScope();
            Bind<IEmployeeRepository>().To<EmployeeRepository>().InRequestScope();
            Bind<CubedContext>().ToSelf().InRequestScope();
        }
    }
}
