using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Controllers;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Client;

namespace Rental.Tests
{
    /// <summary>
    /// Testing client controller
    /// </summary>
    [TestClass]
    public class ClientControllerTest
    {
        private Mock<IClientService> _mockClient;

        private Mock<IRentMapperDM> _mockRentMapper;

        private Mock<IIdentityMapperDM> _mockIdentityMapper;

        private Mock<IRentService> _mockRent;

        private Mock<ILogWriter> _mockLog;

        private ClientController _controller;

        /// <summary>
        /// Initialization
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<IClientService>();
            _mockRentMapper = new Mock<IRentMapperDM>();
            _mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            _mockRentMapper.Setup(x => x.ToPaymentDM.Map<IEnumerable<PaymentDTO>, List<PaymentDM>>(new List<PaymentDTO>()))
             .Returns(new List<PaymentDM>());
            _mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM());
            _mockIdentityMapper = new Mock<IIdentityMapperDM>();
            _mockRent = new Mock<IRentService>();
            _mockLog = new Mock<ILogWriter>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            _controller = new ClientController(_mockClient.Object, _mockIdentityMapper.Object,
                _mockRentMapper.Object, _mockRent.Object, _mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
        }

        /// <summary>
        /// The test that create profile view is createProfile.cshtml
        /// </summary>
        [TestMethod]
        public async Task CreateProfileViewEqualCreateProfileCshtml()
        {
            ViewResult result = await _controller.CreateProfile() as ViewResult;

            Assert.AreEqual(result.ViewName, "CreateProfile");
        }

        /// <summary>
        /// The test that create profile redirect to showProfile
        /// </summary>
        [TestMethod]
        public void CreateProfileRedirectToShowProfile()
        {
            var model = new ProfileDM()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                DateOfExpiry = DateTime.Now.AddYears(1),
                DateOfIssue = DateTime.Now.AddYears(-1)
            };

            _mockIdentityMapper.Setup(x => x.ToProfileDTO.Map<ProfileDM, ProfileDTO>(model))
                .Returns(new ProfileDTO());

            RedirectToRouteResult result = _controller.CreateProfile(model) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "ShowProfile");
        }

        /// <summary>
        /// The test that update profile view is customNotFount.cshtml
        /// </summary>
        [TestMethod]
        public async Task UpdateProfileViewEqualCustomNotFoundCshtml()
        {
            ViewResult result = await _controller.UpdateProfile() as ViewResult;

            Assert.AreEqual(result.ViewName, "CustomNotFound");
        }

        /// <summary>
        /// The test that update profile redirect to showProfile
        /// </summary>
        [TestMethod]
        public void UpdateProfileRedirectToShowProfile()
        {
            var model = new ProfileDM()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                DateOfExpiry = DateTime.Now.AddYears(1),
                DateOfIssue = DateTime.Now.AddYears(-1)
            };

            _mockIdentityMapper.Setup(x => x.ToProfileDTO.Map<ProfileDM, ProfileDTO>(model))
                .Returns(new ProfileDTO());

            RedirectToRouteResult result = _controller.UpdateProfile(model) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "ShowProfile");
        }

        /// <summary>
        /// The test that show profile redirect to createProfile
        /// </summary>
        [TestMethod]
        public async Task ShowProfileRedirectToCreateProfile()
        {
            RedirectToRouteResult result = await _controller.ShowProfile() as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "CreateProfile");
        }

        /// <summary>
        /// The test that show user orders view result not null
        /// </summary>
        [TestMethod]
        public void ShowUserOrdersViewResultNotNull()
        {
            ViewResult result = _controller.ShowUserOrders(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that show user orders model not null
        /// </summary>
        [TestMethod]
        public void ShowUserOrdersModelNotNull()
        {

            ViewResult result = _controller.ShowUserOrders(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ShowUserOrdersVM));
        }

        /// <summary>
        /// The test that show user orders view is showUserOrders.cshtml
        /// </summary>
        [TestMethod]
        public void ShowUserOrdersViewResultEqualShowUserOrdersCshtml()
        {
            ViewResult result = _controller.ShowUserOrders(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowUserOrders");
        }

        /// <summary>
        /// The test that make order view result is customNotFount.cshtml
        /// </summary>
        [TestMethod]
        public async Task MakeOrderViewResultEqualCustomNotFoundCshtml()
        {
            ViewResult result = await _controller.MakeOrder( 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "CustomNotFound");
        }

        /// <summary>
        /// The test that make order view model nit null
        /// </summary>
        [TestMethod]
        public async Task MakeOrderViewModelNotNull()
        {
            ViewResult result = await _controller.MakeOrder(new OrderDM() {Car=new CarDM() { Id=1} }) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(OrderDM));
        }

        /// <summary>
        /// The test that make order view is makeOrder.cshtml
        /// </summary>
        [TestMethod]
        public async Task MakeOrderViewResultEqualMakeOrderCshtml()
        {
            ViewResult result = await _controller.MakeOrder(new OrderDM() { Car = new CarDM() { Id = 1 } }) as ViewResult;

            Assert.AreEqual(result.ViewName, "MakeOrder");
        }

        /// <summary>
        /// The test that make payment model not null
        /// </summary>
        [TestMethod]
        public void MakePaymentViewModelNotNull()
        {
            var model = new PaymentDTO();

            _mockClient.Setup(x => x.GetPayment(1)).Returns(model);
            _mockRentMapper.Setup(x => x.ToPaymentDM.Map < PaymentDTO, PaymentDM > (model))
                .Returns(new PaymentDM());

            ViewResult result =  _controller.MakePayment(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(PaymentDM));
        }

        /// <summary>
        /// The test that make payment redirect to success page
        /// </summary>
        [TestMethod]
        public void MakePaymentRedirectToSuccess()
        {
            var model = new PaymentDTO();
            _mockClient.Setup(x => x.GetPayment(1)).Returns(model);
            ViewResult result = _controller.MakePayment(new PaymentDM()) as ViewResult;

            Assert.AreEqual(result.ViewName,"SuccessPay");
        }

        /// <summary>
        /// The test that show payments model not null
        /// </summary>
        [TestMethod]
        public void ShowPaymentsModelNotNull()
        {
            ViewResult result = _controller.ShowPayments(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ShowPaymentsVM));
        }

        /// <summary>
        /// The test that show payments  view is showPayments.cshtml
        /// </summary>
        [TestMethod]
        public void ShowPaymentsViewResultEqualShowPaymentsCshtml()
        {
            ViewResult result = _controller.ShowPayments(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowPayments");
        }
    }
}
