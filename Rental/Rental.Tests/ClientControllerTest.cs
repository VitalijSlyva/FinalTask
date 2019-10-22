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
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public async Task CreateProfileViewEqualCreateProfileCshtml()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object,mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.CreateProfile() as ViewResult;

            Assert.AreEqual(result.ViewName, "CreateProfile");
        }

        [TestMethod]
        public void CreateProfileRedirectToShowProfile()
        {
            var model = new ProfileDM()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                DateOfExpiry = DateTime.Now.AddYears(1),
                DateOfIssue = DateTime.Now.AddYears(-1)
            };
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            mockIdentityMapper.Setup(x => x.ToProfileDTO.Map<ProfileDM, ProfileDTO>(model))
                .Returns(new ProfileDTO());
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            RedirectToRouteResult result = controller.CreateProfile(model) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "ShowProfile");
        }

        [TestMethod]
        public async Task UpdateProfileViewEqualCustomNotFoundCshtml()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var profileModel = new ProfileDTO();
            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.UpdateProfile() as ViewResult;

            Assert.AreEqual(result.ViewName, "CustomNotFound");
        }

        [TestMethod]
        public void UpdateProfileRedirectToShowProfile()
        {
            var model = new ProfileDM()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                DateOfExpiry = DateTime.Now.AddYears(1),
                DateOfIssue = DateTime.Now.AddYears(-1)
            };
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            mockIdentityMapper.Setup(x => x.ToProfileDTO.Map<ProfileDM, ProfileDTO>(model))
                .Returns(new ProfileDTO());
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            RedirectToRouteResult result = controller.UpdateProfile(model) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "ShowProfile");
        }

        [TestMethod]
        public async Task ShowProfileRedirectToCreateProfile()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            RedirectToRouteResult result = await controller.ShowProfile() as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "CreateProfile");
        }


        [TestMethod]
        public async Task ShowUserOrdersViewResultNotNull()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.ShowUserOrders(null,0,0,1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public async Task ShowUserOrdersModelNotNull()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.ShowUserOrders(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ShowUserOrdersVM));
        }

        [TestMethod]
        public async Task ShowUserOrdersViewResultEqualShowUserOrdersCshtml()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.ShowUserOrders(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowUserOrders");
        }

        [TestMethod]
        public async Task MakeOrderViewResultEqualCustomNotFoundCshtml()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.MakeOrder( 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "CustomNotFound");
        }

        [TestMethod]
        public async Task MakeOrderViewModelIsNotNull()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.MakeOrder(new OrderDM() {Car=new CarDM() { Id=1} }) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(OrderDM));
        }

        [TestMethod]
        public async Task MakeOrderViewResultEqualMakeOrderCshtml()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = await controller.MakeOrder(new OrderDM() { Car = new CarDM() { Id = 1 } }) as ViewResult;

            Assert.AreEqual(result.ViewName, "MakeOrder");
        }

        [TestMethod]
        public void MakePaymentViewModelIsNotNull()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var model = new PaymentDTO();
            var mockClient = new Mock<IClientService>();
            mockClient.Setup(x => x.GetPayment(1)).Returns(model);
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToPaymentDM.Map < PaymentDTO, PaymentDM > (model))
                .Returns(new PaymentDM());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result =  controller.MakePayment(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(PaymentDM));
        }

        [TestMethod]
        public void MakePaymentRedirectToIndex()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var model = new PaymentDTO();
            var mockClient = new Mock<IClientService>();
            mockClient.Setup(x => x.GetPayment(1)).Returns(model);
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            RedirectToRouteResult result = controller.MakePayment(new PaymentDM()) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"],"Index");
        }

        [TestMethod]
        public void ShowPaymentsOrdersModelNotNull()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToPaymentDM.Map<IEnumerable<PaymentDTO>, List<PaymentDM>>(new List<PaymentDTO>()))
                .Returns(new List<PaymentDM>());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = controller.ShowPayments(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ShowPaymentsVM));
        }

        [TestMethod]
        public void ShowPaymentsOrdersViewResultEqualShowPaymentsCshtml()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var mockClient = new Mock<IClientService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToPaymentDM.Map<IEnumerable<PaymentDTO>, List<PaymentDM>>(new List<PaymentDTO>()))
                .Returns(new List<PaymentDM>());
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            ClientController controller = new ClientController(mockClient.Object, mockIdentityMapper.Object,
                mockRentMapper.Object, mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
            ViewResult result = controller.ShowPayments(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowPayments");
        }
    }
}
