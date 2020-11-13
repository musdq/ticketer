using System.Threading.Tasks;
using E_Ticketer.Models.TokenAuth;
using E_Ticketer.Web.Controllers;
using Shouldly;
using Xunit;

namespace E_Ticketer.Web.Tests.Controllers
{
    public class HomeController_Tests: E_TicketerWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}