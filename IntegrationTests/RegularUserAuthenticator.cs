

using Omu.ProDinner.WebUI.Controllers;
using Omu.ProDinner.WebUI.ViewModels.Input;
using OpenQA.Selenium.Safari.Internal;
using SpecsFor.Mvc;
using SpecsFor.Mvc.Authentication;

namespace IntegrationTests
{
    public class RegularUserAuthenticator : IHandleAuthentication
    {
        public void Authenticate(MvcWebApp mvcWebApp)
        {
            mvcWebApp.NavigateTo<AccountController>(c => c.SignIn());

            mvcWebApp.FindFormFor<SignInInput>()
                .Field(m => m.Login).SetValueTo("o")
                .Field(m => m.Password).SetValueTo("1").Submit();
        }
    }
}