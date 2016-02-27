using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq.Protected;
using Omu.ProDinner.Data;
using Omu.ProDinner.WebUI.App_Start;
using SpecsFor.Mvc;
using StructureMap;

namespace IntegrationTests
{
    public class TestSeedData : SpecsForMvcConfig
    {
        public TestSeedData()
        {
            BeforeEachTest(SetupDatabase);
        }

        private void SetupDatabase()
        {
            Bootstrapper webConfigSetup = new Bootstrapper();
            webConfigSetup.Bootstrap();

            //Empties Db
            Database.SetInitializer(new DropCreateDatabaseAlways<DbContext>());

            //Create new Context
            DbContext newContext = new Db();

            Seed(newContext);
        }


        protected void Seed(DbContext myContext)
        {
            //Seed Database Code Here
        }
        
    }
}
