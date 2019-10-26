using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rental.BLL.Interfaces;
using Rental.WEB.Controllers;
using Rental.WEB.Interfaces;
using System.Web.Mvc;
using Rental.BLL.DTO.Identity;
using System.Collections.Generic;
using Rental.WEB.Models.Domain_Models.Identity;
using System.Threading.Tasks;
using Rental.WEB.Models.View_Models.Admin;
using Rental.BLL.DTO.Rent;
using Rental.WEB.Models.Domain_Models.Rent;
using System.Security.Principal;

namespace Rental.Tests
{
    /// <summary>
    /// Testing admin controller
    /// </summary>
    [TestClass]
    public class AdminControllerTest
    {
        private Mock<IAdminService> _mockAdmin;

        private Mock<IRentMapperDM> _mockRentMapper;

        private Mock<IIdentityMapperDM> _mockIdentityMapper;

        private Mock<IRentService> _mockRent;

        private Mock<ILogWriter> _mockLog;

        private AdminController _controller;

        /// <summary>
        /// Initialization
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockAdmin = new Mock<IAdminService>();
            _mockAdmin.Setup(x => x.GetUsers()).Returns(new List<User>());
            _mockRentMapper = new Mock<IRentMapperDM>();
            _mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM());
            _mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(new CarDTO())).Returns(new CarDM());
            _mockRentMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>()))
                .Returns(new List<CarDM>());
            _mockRentMapper.Setup(x => x.ToImageDTO.Map<List<ImageDM>, IEnumerable<ImageDTO>>(new List<ImageDM>()))
                .Returns(null as List<ImageDTO>);
            _mockIdentityMapper = new Mock<IIdentityMapperDM>();
            _mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
               .Returns(new List<UserDM>());
            _mockRent = new Mock<IRentService>();
            _mockRent.Setup(x => x.GetCar(1)).Returns(new CarDTO());
            _mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            _mockLog = new Mock<ILogWriter>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            _controller = new AdminController(_mockAdmin.Object, _mockIdentityMapper.Object, _mockRentMapper.Object,
                   _mockRent.Object, _mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };
        }

        /// <summary>
        /// The test that get users view result not null
        /// </summary>
        [TestMethod]
        public async Task GetUsersViewResultNotNull()
        {
            ViewResult result = await _controller.GetUsers(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that login model not null
        /// </summary>
        [TestMethod]
        public async Task GetUsersModelNotNull()
        {
            ViewResult result = await _controller.GetUsers(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(GetUsersVM));
        }

        /// <summary>
        /// The test that login view is getUsers.cshtml
        /// </summary>
        [TestMethod]
        public async Task GetUsersViewResultEqualGetUsersCshtml()
        {
            ViewResult result = await _controller.GetUsers(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "GetUsers");
        }

        /// <summary>
        /// The test that banUsers redirect to getUsers
        /// </summary>
        [TestMethod]
        public void BanUserRedirectToGetUsers()
        {
            RedirectToRouteResult result = _controller.BanUser(null) as RedirectToRouteResult;

            Assert.AreEqual("GetUsers", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that unbanUsers redirect to getUsers
        /// </summary>
        [TestMethod]
        public void UnbanUserRedirectToGetUsers()
        {
            RedirectToRouteResult result = _controller.UnbanUser(null) as RedirectToRouteResult;

            Assert.AreEqual("GetUsers", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that create car view is createCar.cshtml
        /// </summary>
        [TestMethod]
        public void CreateCarViewEqualCreateCarCshtml()
        {
            ViewResult result = _controller.CreateCar() as ViewResult;

            Assert.AreEqual("CreateCar", result.ViewName);
        }

        /// <summary>
        /// The test that get cars view result not null
        /// </summary>
        [TestMethod]
        public void GetCarsViewResultNotNull()
        {
            ViewResult result = _controller.GetCars(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that get cars model not null
        /// </summary>
        [TestMethod]
        public void GetCarsModelNotNull()
        {
            ViewResult result = _controller.GetCars(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(GetCarsVM));
        }

        /// <summary>
        /// The test that view is getCars.cshtml
        /// </summary>
        [TestMethod]
        public void GetCarsViewResultEqualGetCarsCshtml()
        {
            ViewResult result = _controller.GetCars(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "GetCars");
        }

        /// <summary>
        /// The test that create car retirect to getCars
        /// </summary>
        [TestMethod]
        public void CreateCarViewRedirectToGetCars()
        {
            var model = new CreateVM()
            {
                Car = new CarDM() { DateOfCreate = DateTime.Now }
            };

            _mockRentMapper.Setup(x => x.ToCarDTO.Map<CarDM, CarDTO>(model.Car)).Returns(new CarDTO());

            RedirectToRouteResult result = _controller.CreateCar(model) as RedirectToRouteResult;

            Assert.AreEqual("GetCars", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that update car model not null
        /// </summary>
        [TestMethod]
        public void UpdateCarModelCarNotNull()
        {
            var model = new CarDTO() { };
            _mockRent.Setup(x => x.GetCar(1)).Returns(model);
            _mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(model)).Returns(new CarDM());

            ViewResult result = _controller.UpdateCar(1) as ViewResult;

            Assert.IsNotNull((result.Model as CreateVM).Car);
        }

        /// <summary>
        /// The test that update car view is updateCar.cshtml
        /// </summary>
        [TestMethod]
        public void UpdateCarViewResultEqualUpdateCarCshtml()
        {

            ViewResult result = _controller.UpdateCar(1) as ViewResult;

            Assert.AreEqual(result.ViewName,"CreateCar");
        }

        /// <summary>
        /// The test that updateCar redirect to getCars
        /// </summary>
        [TestMethod]
        public void UpdateCarViewRedirectToGetCars()
        {
            var model = new CreateVM()
            {
                Car = new CarDM() { DateOfCreate = DateTime.Now }
            };

            _mockRentMapper.Setup(x => x.ToCarDTO.Map<CarDM, CarDTO>(model.Car)).Returns(new CarDTO());

            RedirectToRouteResult result = _controller.UpdateCar(model) as RedirectToRouteResult;

            Assert.AreEqual("GetCars", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that delete redirect to getCars
        /// </summary>
        [TestMethod]
        public void DeleteViewRedirectToGetCars()
        {
            RedirectToRouteResult result = _controller.Delete(1) as RedirectToRouteResult;

            Assert.AreEqual("GetCars", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that create manager view is register.cshtml
        /// </summary>
        [TestMethod]
        public void CreateManagerViewEqualRegisterCshtml()
        {
            ViewResult result = _controller.CreateManager() as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }

        /// <summary>
        /// The test that create manager redirect to getUsers
        /// </summary>
        [TestMethod]
        public void CreateManagerViewRedirectToGetUsers()
        {
            RedirectToRouteResult result = _controller.CreateManager(new WEB.Models.View_Models.Account.RegisterVM())
                as RedirectToRouteResult;

            Assert.AreEqual("GetUsers", result.RouteValues["action"]);
        }

        /// <summary>
        /// The test that autocompleteBrand result not null
        /// </summary>
        [TestMethod]
        public void AutocompleteBrandResultNotNull()
        {
            JsonResult result = _controller.AutocompleteBrand("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        /// <summary>
        /// The test that autocompleteCarcass result not null
        /// </summary>
        [TestMethod]
        public void AutocompleteCarcassResultNotNull()
        {
            JsonResult result = _controller.AutocompleteCarcass("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        /// <summary>
        /// The test that autocompleteQuality result not null
        /// </summary>
        [TestMethod]
        public void AutocompleteQualityResultNotNull()
        {
            JsonResult result = _controller.AutocompleteQuality("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        /// <summary>
        /// The test that autocompleteTransmission result not null
        /// </summary>
        [TestMethod]
        public void AutocompleteTransmissionResultNotNull()
        {
            JsonResult result = _controller.AutocompleteTransmission("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        /// <summary>
        /// The test that autocompletePropertyName not null
        /// </summary>
        [TestMethod]
        public void AutocompletePropertyNameNotNull()
        {
            JsonResult result = _controller.AutocompletePropertyName("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        /// <summary>
        /// The test that autocompletePropertyValue not null
        /// </summary>
        [TestMethod]
        public void AutocompletePropertyValueNotNull()
        {
            JsonResult result = _controller.AutocompletePropertyValue("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }
    }
}
