using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Controllers;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Manager;

namespace Rental.Tests
{
    [TestClass]
    public class ManagerControllerTest
    {
        [TestMethod]
        public void ShowConfirmsViewResultNotNull()
        {
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetForConfirms()).Returns(new List<OrderDTO>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.ShowConfirms(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public void ShowConfirmsViewModelNotNull()
        {
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetForConfirms()).Returns(new List<OrderDTO>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.ShowConfirms(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(ShowConfirmsVM));
        }

        [TestMethod]
        public void ShowConfirmsViewResultEqualShowConfirmsCshtml()
        {
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetForConfirms()).Returns(new List<OrderDTO>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.ShowConfirms(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowConfirms");
        }

        [TestMethod]
        public void ShowReturnsViewResultNotNull()
        {
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetForReturns()).Returns(new List<OrderDTO>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.ShowReturns(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public void ShowReturnsViewModelNotNull()
        {
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetForReturns()).Returns(new List<OrderDTO>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.ShowReturns(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ShowReturnsVM));
        }

        [TestMethod]
        public void ShowReturnsViewResultEqualShowConfirmsCshtml()
        {
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetForReturns()).Returns(new List<OrderDTO>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
                .Returns(new List<OrderDM>());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.ShowReturns(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowReturns");
        }

        [TestMethod]
        public void ConfirmViewModelNotNull()
        {
            var model = new OrderDTO();
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetOrder(1,true)).Returns(model);
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<OrderDTO, OrderDM>(model))
                .Returns(new OrderDM());
            mockIdentityMapper.Setup(x => x.ToProfileDM.Map<ProfileDTO, ProfileDM>(null)).Returns(new ProfileDM());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.Confirm(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ConfirmDM));
        }

        [TestMethod]
        public void ReturnViewModelNotNull()
        {
            var model = new OrderDTO();
            var mockManager = new Mock<IManagerService>();
            mockManager.Setup(x => x.GetOrder(1,false)).Returns(model);
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToOrderDM.Map<OrderDTO, OrderDM>(model))
                .Returns(new OrderDM());
            mockIdentityMapper.Setup(x => x.ToProfileDM.Map<ProfileDTO, ProfileDM>(null)).Returns(new ProfileDM());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object);

            ViewResult result = controller.Return(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ReturnDM));
        }

        [TestMethod]
        public void ReturnsViewRedirectToShowReturns()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var model = new ReturnDM();
            var mockManager = new Mock<IManagerService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToReturnDTO.Map<ReturnDM, ReturnDTO>(model))
                .Returns(new ReturnDTO() { Order=new OrderDTO() { Id=1} });
            mockIdentityMapper.Setup(x => x.ToProfileDM.Map<ProfileDTO, ProfileDM>(null)).Returns(new ProfileDM());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object)
            {
                ControllerContext = controllerContext.Object
            };

            RedirectToRouteResult result = controller.Return(model,false) as RedirectToRouteResult;

            Assert.AreEqual("ShowReturns", result.RouteValues["action"]);
        }

        [TestMethod]
        public void ConfirmViewRedirectToShowConfirms()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var model = new ConfirmDM() { IsConfirmed = true };
            var mockManager = new Mock<IManagerService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockLog = new Mock<ILogWriter>();
            mockRentMapper.Setup(x => x.ToConfirmDTO.Map<ConfirmDM, ConfirmDTO>(model))
                .Returns(new ConfirmDTO() { Order = new OrderDTO() { Id = 1 } });
            mockIdentityMapper.Setup(x => x.ToProfileDM.Map<ProfileDTO, ProfileDM>(null)).Returns(new ProfileDM());
            ManagerController controller = new ManagerController(mockManager.Object,
                mockRentMapper.Object, mockLog.Object, mockIdentityMapper.Object)
            {
                ControllerContext = controllerContext.Object
            };

            RedirectToRouteResult result = controller.Confirm(model) as RedirectToRouteResult;

            Assert.AreEqual("ShowConfirms", result.RouteValues["action"]);
        }
    }
}
