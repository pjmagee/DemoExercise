using System.Web.Mvc;
using FluentValidation.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Modules;
using Ninject.Web.Mvc.FluentValidation;
using PurpleCubed.DataAccess.EntityFramework;
using PurpleCubed.DataAccess.NHibernate;
using PurpleCubed.UI.Validation;

using System;
using System.Web;
using Ninject;
using Ninject.Web.Common;


[assembly: WebActivator.PreApplicationStartMethod(typeof(PurpleCubed.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(PurpleCubed.UI.App_Start.NinjectWebCommon), "Stop")]

namespace PurpleCubed.UI.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            // You can swap out modules for different implementations 
            // because it is designed with an abstraction layer

            var modules = new NinjectModule[]
                {
                    new FluentValidatorModule(), 
                    new EntityFrameworkModule(),
                    // new NHibernateModule(), needs more work done
                };

            kernel.Load(modules); // These modules define the interfaces to be injected
            // Ninject will handle the object lifetime
            // This means it will create new or inject current instances from its container into 
            // anything that consumes a defined interface and annotated with [Inject]

            // https://github.com/ninject/ninject.web.mvc.fluentvalidation

            // FluentValidation plugin goes well
            NinjectValidatorFactory ninjectValidatorFactory = new NinjectValidatorFactory(kernel);
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(ninjectValidatorFactory));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }        
    }
}
