using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using NUnit.Framework;
using Omu.ProDinner.WebUI.Controllers;
using Omu.ProDinner.WebUI.ViewModels.Input;
using SpecsFor.Mvc;
using SpecsFor;
using SpecsFor.Helpers.Web.Mvc;

namespace IntegrationTests.General
{
    public class AuthenticationSpecs
    {
        public  abstract class  given_the_user_is_UNauthenticated : SpecsFor<MvcWebApp>
        {
            protected override void Given()
            {
                SUT.NavigateTo<AccountController>(c => c.SignOff());
            }

            public class when_trying_to_access_the_home_controller : given_the_user_is_UNauthenticated
            {
                protected override void When()
                {
                    SUT.NavigateTo<HomeController>(c => c.Index());
                }

                [Test]
                public void then_it_redirects_to_login_page()
                {
                    SUT.Route.ShouldMapTo<AccountController>(c => c.SignIn());
                }
            }

            public class when_logging_in_with_no_credentials : given_the_user_is_UNauthenticated
            {
                protected override void When()
                {
                    SUT.NavigateTo<AccountController>(c => c.SignIn());
                    SUT.FindFormFor<SignInInput>().Submit();
                }
            }

            [Test]
            public void then_it_displays_a_validation_error_on_the_username()
            {
                SUT.FindFormFor<SignInInput>() 
                    .Field(f => f.Login).ShouldBeInvalid();
            }

            [Test]
            public void then_it_displays_a_validation_error_on_the_password()
            {
                SUT.FindFormFor<SignInInput>() 
                    .Field(f => f.Password).ShouldBeInvalid();
            }

            public class when_loggin_in_with_invalid_credentials : given_the_user_is_UNauthenticated
            {
                protected override void When()
                {
                    SUT.NavigateTo<AccountController>(c => c.SignIn());
                    SUT.FindFormFor<SignInInput>()
                        .Field(f => f.Login).SetValueTo("failure")
                        .Field(f => f.Password).SetValueTo("Blah").Submit();

                }

                [Test]
                public void then_it_should_not_log_you_in()
                {
                    SUT.Route.ShouldMapTo<AccountController>(c => c.SignIn());
                }
            }

            public class when_logging_in_with_valid_creditials : given_the_user_is_UNauthenticated
            {
                protected override void When()
                {
                    SUT.NavigateTo<AccountController>(c => c.SignIn());
                    SUT.FindFormFor<SignInInput>()
                        .Field(f => f.Login).SetValueTo("o")
                        .Field(f => f.Password).SetValueTo("1").Submit();

                }

                [Test]
                public void thenshould_redirect_to_home()
                {
                    SUT.Route.ShouldMapTo<HomeController>(c => c.Index());
                }
            }
        }
    }
}
