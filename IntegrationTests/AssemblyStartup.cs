using NUnit.Framework;
using Omu.ProDinner.WebUI.App_Start;
using SpecsFor;
using SpecsFor.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class AssemblyStartup
    {
        [SetUp]
        public void Start()
        {
            Bootstrapper routeCongiurator = new Bootstrapper();

            var config = new SpecsForMvcConfig();
            config.UseIISExpress()
                .With(Project.Named("WebUI"));

            config.BuildRoutesUsing(r => routeCongiurator.Bootstrap());
        }
    }
}
