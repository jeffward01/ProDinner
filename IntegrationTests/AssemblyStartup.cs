using NUnit.Framework;
using Omu.ProDinner.WebUI.App_Start;
using SpecsFor;
using SpecsFor.Mvc;
using SpecsFor.Helpers.Web;
using SpecsFor.Helpers.Web.Mvc;
using SpecsFor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [SetUpFixture]
    public class AssemblyStartup
    {
        private SpecsForIntegrationHost _host;

        [SetUp]
        public void Start()
        {
            Bootstrapper routeCongiurator = new Bootstrapper();

            var config = new SpecsForMvcConfig();
            config.UseIISExpress()
                .With(Project.Named("WebUI"));

            config.BuildRoutesUsing(r => routeCongiurator.Bootstrap());
            config.Use<TestSeedData>();
            config.AuthenticateBeforeEachTestUsing<RegularUserAuthenticator>();

            _host = new SpecsForIntegrationHost(config);
            _host.Start();
        }

        [TearDown]
        public void Stop()
        {
            _host.Shutdown();

        }
    }
}
