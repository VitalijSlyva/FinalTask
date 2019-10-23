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
    /// <summary>
    /// Testing manager controller
    /// </summary>
    [TestClass]
    public class ManagerControllerTest
    {
        private Mock<IManagerService> _mockManager;
        private Mock<IRentMapperDM> _mockRentMapper;
        private Mock<IIdentityMapperDM> _mockIdentityMapper;
        private Mock<ILogWriter> _mockLog;
        private ManagerController _controller;

        /// <summary>
        /// Initialization
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockManager = new Mock<IManagerService>();
            _mockManager.Setup(x => x.GetForReturns()).Returns(new List<OrderDTO>());
            _mockRentMapper = new Mock<IRentMapperDM>();
            _mockManager.Setup(x => x.GetForConfirms()).Returns(new List<OrderDTO>());
            _mockRentMapper.Setup(x => x.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(new List<OrderDTO>()))
               .Returns(new List<OrderDM>());
            _mockIdentityMapper = new Mock<IIdentityMapperDM>();
            _mockIdentityMapper.Setup(x => x.ToProfileDM.Map<ProfileDTO, ProfileDM>(null)).Returns(new ProfileDM());
            _mockLog = new Mock<ILogWriter>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            _controller = new ManagerController(_mockManager.Object,
                _mockRentMapper.Object, _mockLog.Object, _mockIdentityMapper.Object)
            {
                ControllerContext = controllerContext.Object
            };
        }

        /// <summary>
        /// The test that show confirms view result not null
        /// </summary>
        [TestMethod]
        public void ShowConfirmsViewResultNotNull()
        {
            ViewResult result = _controller.ShowConfirms(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that show confirms view model not null
        /// </summary>
        [TestMethod]
        public void ShowConfirmsViewModelNotNull()
        {
            ViewResult result = _controller.ShowConfirms(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(ShowConfirmsVM));
        }

        /// <summary>
        /// The test that show confirms view is showConfirms.cshtml
        /// </summary>
        [TestMethod]
        public void ShowConfirmsViewResultEqualShowConfirmsCshtml()
        {
            ViewResult result = _controller.ShowConfirms(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowConfirms");
        }

        /// <summary>
        /// The test that show returns view result not null
        /// </summary>
        [TestMethod]
        public void ShowReturnsViewResultNotNull()
        {
            ViewResult result = _controller.ShowReturns(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that show returns view model not null
        /// </summary>
        [TestMethod]
        public void ShowReturnsViewModelNotNull()
        {
            ViewResult result = _controller.ShowReturns(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ShowReturnsVM));
        }

        /// <summary>
        /// The test that show returns view showConfirms.cshtml
        /// </summary>
        [TestMethod]
        public void ShowReturnsViewResultEqualShowConfirmsCshtml()
        {
            ViewResult result = _controller.ShowReturns(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "ShowReturns");
        }

        /// <summary>
        /// The test that confirm view model not null
        /// </summary>
        [TestMethod]
        public void ConfirmViewModelNotNull()
        {
            var model = new OrderDTO();

            _mockManager.Setup(x => x.GetOrder(1,true)).Returns(model);
            _mockRentMapper.Setup(x => x.ToOrderDM.Map<OrderDTO, OrderDM>(model))
                .Returns(new OrderDM());

            ViewResult result = _controller.Confirm(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ConfirmDM));
        }

        /// <summary>
        /// The test that return view model not null
        /// </summary>
        [TestMethod]
        public void ReturnViewModelNotNull()
        {
            var model = new OrderDTO();
            _mockManager.Setup(x => x.GetOrder(1,false)).Returns(model);
            _mockRentMapper.Setup(x => x.ToOrderDM.Map<OrderDTO, OrderDM>(model))
                .Returns(new OrderDM());

            ViewResult result = _controller.Return(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ReturnDM));
        }

        /// <summary>
        /// The test that returns view redirect to showReturns
        /// </summary>
        [TestMethod]
        public void ReturnsViewRedirectToShowReturns()
        {
            var model = new ReturnDM();
            _mockRentMapper.Setup(x => x.ToReturnDTO.Map<ReturnDM, ReturnDTO>(model))
                .Returns(new ReturnDTO() { Order=new OrderDTO() { Id=1} });

            RedirectToRouteResult result = _controller.Return(model,false) as RedirectToRouteResult;

            Assert.AreEqual("ShowReturns", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that confirm view redirect to showConfirms.cshtml 
        /// </summary>
        [TestMethod]
        public void ConfirmViewRedirectToShowConfirms()
        {
            var model = new ConfirmDM() { IsConfirmed = true };
            _mockRentMapper.Setup(x => x.ToConfirmDTO.Map<ConfirmDM, ConfirmDTO>(model))
                .Returns(new ConfirmDTO() { Order = new OrderDTO() { Id = 1 } });

            RedirectToRouteResult result = _controller.Confirm(model) as RedirectToRouteResult;

            Assert.AreEqual("ShowConfirms", result.RouteValues["action"]);
        }
    }
}
