using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rental.BLL.Interfaces;
using Rental.WEB.Controllers;
using Rental.WEB.Interfaces;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Security.Principal;
using Rental.BLL.DTO.Identity;
using Rental.WEB.Models.Domain_Models.Identity;

namespace Rental.Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void LoginViewResultNotNull()
        {
            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object);

            ViewResult result = controller.Login() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginViewEqualLoginCshtml()
        {
            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object,mockIdentity.Object,mockLog.Object);

            ViewResult result = controller.Login() as ViewResult;

            Assert.AreEqual("Login", result.ViewName);
        }

        [TestMethod]
        public void RegisterViewResultNotNull()
        {
            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object);

            ViewResult result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterViewEqualRegisterCshtml()
        {
            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object);

            ViewResult result = controller.Register() as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public async Task LoginModelEqual()
        {
            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object);

            ViewResult result = await controller.Login(new WEB.Models.View_Models.Account.LoginVM()) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(WEB.Models.View_Models.Account.LoginVM));
        }

        [TestMethod]
        public async Task RegisterModelEqual()
        {
            var mockAccount = new Mock<IAccountService>();
            mockAccount.Setup(x => x.CreateAsync(new BLL.DTO.Identity.User())).Returns(new Task<string>(()=>" "));
            var mockIdentity = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object);

            ViewResult result = await controller.Register(new WEB.Models.View_Models.Account.RegisterVM()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(WEB.Models.View_Models.Account.RegisterVM));
        }

        [TestMethod]
        public async Task ShowUserModelEqual()
        {

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            mockIdentity.Setup(x => x.ToUserDM.Map<User, UserDM>(null)).Returns(new UserDM());
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.ShowUser() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(WEB.Models.Domain_Models.Identity.UserDM));
        }

        [TestMethod]
        public async Task ShowUserEqualShowUserCshtml()
        {

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            mockIdentity.Setup(x => x.ToUserDM.Map<User, UserDM>(null)).Returns(new UserDM());
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.ShowUser() as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowUser");
        }

        [TestMethod]
        public async Task ShowUserViewResultNotNull()
        {

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockAccount = new Mock<IAccountService>();
            var mockIdentity = new Mock<IIdentityMapperDM>();
            mockIdentity.Setup(x => x.ToUserDM.Map<User, UserDM>(null)).Returns(new UserDM());
            var mockLog = new Mock<ILogWriter>();
            AccountController controller = new AccountController(mockAccount.Object, mockIdentity.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.ShowUser() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
