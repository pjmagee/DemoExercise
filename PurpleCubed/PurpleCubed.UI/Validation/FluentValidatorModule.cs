using System.Reflection;
using FluentValidation;
using Ninject.Modules;

namespace PurpleCubed.UI.Validation
{
    /// <summary>
    /// http://fluentvalidation.codeplex.com/
    /// https://github.com/ninject/ninject.web.mvc.fluentvalidation/issues/3
    /// http://fluentvalidation.codeplex.com/documentation
    /// </summary>
    public class FluentValidatorModule : NinjectModule
    {
        // Using Reflection load all validators in the assembly (Team and Employee)

        public override void Load()
        {
            // ~/Validation/*Validator.cs
            AssemblyScanner assembly = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());

            // Bind these for Ninject Kernel
            foreach (AssemblyScanner.AssemblyScanResult result in assembly)
            {
                Bind(result.InterfaceType).To(result.ValidatorType);
            }
        }
    }
}