using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace PurpleCubed.UI.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterConfig()
        {
            Mapper.Initialize(config => GetConfiguration(Mapper.Configuration));
        }

        // Using Reflection load all the profiles which contain the AutoMapper configuration
        // from entities to view models visa versa
        // to be used if applying ViewModels in a mature application so that entity models are not 
        // tighly coupled to the UI layer 

        private static void GetConfiguration(IConfiguration configuration)
        {
            configuration.AllowNullDestinationValues = true;
            configuration.AllowNullCollections = true;

            IEnumerable<Type> profiles = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(Profile).IsAssignableFrom(type));

            foreach (var profile in profiles)
            {
                configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }
    }
}