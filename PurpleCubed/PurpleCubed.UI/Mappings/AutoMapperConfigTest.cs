using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NUnit.Framework;
using PurpleCubed.UI.App_Start;

namespace PurpleCubed.UI.Mappings
{
    [TestFixture]
    public class AutoMapperConfigTest
    {

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AutoMapperConfig.RegisterConfig();
        }

        [Test]
        public void ShouldMapEverything()
        {
            Mapper.AssertConfigurationIsValid();
        }

    }
}