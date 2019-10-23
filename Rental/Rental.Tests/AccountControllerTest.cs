using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rental.BLL.Interfaces;
using Rental.WEB.Controllers;
using Rental.WEB.Interfaces;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Security.Principal;
using Rental.BLL.DTO.Identity;
using Rental.WEB.Models.Domain_Models.Identity;

namespace Rental.Tests
{
    /// <summary>
    /// Testing account controller
    /// </summary>
    [TestClass]
    public class AccountControllerTest
    {
        private Mock<IAccountService> _mockAccount;
        private Mock<IIdentityMapperDM> _mockIdentity;
        private Mock<ILogWriter> _mockLog;
        private AccountController _controller;

        /// <summary>
        /// Initialization
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockAccount = new Mock<IAccountService>();
            _mockAccount.Setup(x => x.CreateAsync(new BLL.DTO.Identity.User())).Returns(new Task<string>(() => " "));
            _mockIdentity = new Mock<IIdentityMapperDM>();
            _mockIdentity.Setup(x => x.ToUserDM.Map<User, UserDM>(null)).Returns(new UserDM());
            _mockLog = new Mock<ILogWriter>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            _controller = new AccountController(_mockAccount.Object, _mockIdentity.Object, _mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
        }

        /// <summary>
        /// The test that login view result not null
        /// </summary>
        [TestMethod]
        public void LoginViewResultNotNull()
        {
            ViewResult result = _controller.Login() as ViewResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// The test that login view is login.cshtml
        /// </summary>
        [TestMethod]
        public void LoginViewEqualLoginCshtml()
        {
            ViewResult result = _controller.Login() as ViewResult;

            Assert.AreEqual("Login", result.ViewName);
        }

        /// <summary>
        /// The test that register view result not null
        /// </summary>
        [TestMethod]
        public void RegisterViewResultNotNull()
        {
            ViewResult result = _controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// The test that register view is register.cshtml 
        /// </summary>
        [TestMethod]
        public void RegisterViewEqualRegisterCshtml()
        {
            ViewResult result = _controller.Register() as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }

        /// <summary>
        /// The test that  login model is loginVM
        /// </summary>
        [TestMethod]
        public async Task LoginModelEqual()
        {
            ViewResult result = await _controller.Login(new WEB.Models.View_Models.Account.LoginVM()) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(WEB.Models.View_Models.Account.LoginVM));
        }

        /// <summary>
        /// The test that register model is registerVM
        /// </summary>
        [TestMethod]
        public async Task RegisterModelEqual()
        {
            ViewResult result = await _controller.Register(new WEB.Models.View_Models.Account.RegisterVM()) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(WEB.Models.View_Models.Account.RegisterVM));
        }

        /// <summary>
        /// The test that  show user model is userDM
        /// </summary>
        [TestMethod]
        public async Task ShowUserModelEqual()
        {
            ViewResult result = await _controller.ShowUser() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(WEB.Models.Domain_Models.Identity.UserDM));
        }

        /// <summary>
        /// The test that show user view is showUser.cshtml
        /// </summary>
        [TestMethod]
        public async Task ShowUserEqualShowUserCshtml()
        {
            ViewResult result = await _controller.ShowUser() as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowUser");
        }

        /// <summary>
        /// The test that show user view result not null
        /// </summary>
        [TestMethod]
        public async Task ShowUserViewResultNotNull()
        {
            ViewResult result = await _controller.ShowUser() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
