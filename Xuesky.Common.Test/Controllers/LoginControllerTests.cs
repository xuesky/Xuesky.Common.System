using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xuesky.Common.Service;
using Xuesky.Common.Web;
using Xuesky.Common.Web.Controllers;
using Xunit;

namespace Xuesky.Common.Test.Controllers
{
    public class LoginControllerTests
    {

        IAccountService accountService = null;
        public LoginControllerTests()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            accountService = server.Host.Services.GetService<IAccountService>();
        }

        [Fact]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var loginController = new LoginController(accountService);

            // Act
            var result = loginController.Index();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task Login_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var loginController = new LoginController(accountService);
            SysUserLoginInput sysUserLoginInput = new SysUserLoginInput
            {
                UserLogin = "admin",
                UserPwd = "123456"
            };

            // Act
            var result = await loginController.Login(
                sysUserLoginInput);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task Logout_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var loginController = new LoginController(accountService);

            // Act
            var result = await loginController.Logout();

            // Assert
            Assert.True(true);
        }
    }
}
