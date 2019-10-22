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
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public async Task GetUsersViewResultNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            mockAdmin.Setup(x => x.GetUsers()).Returns(new List<User>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
                .Returns(new List<UserDM>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = await controller.GetUsers(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public async Task GetUsersModelNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            mockAdmin.Setup(x => x.GetUsers()).Returns(new List<User>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
                .Returns(new List<UserDM>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = await controller.GetUsers(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(GetUsersVM));
        }

        [TestMethod]
        public async Task GetUsersViewResultEqualGetUsersCshtml()
        {
            var mockAdmin = new Mock<IAdminService>();
            mockAdmin.Setup(x => x.GetUsers()).Returns(new List<User>());
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
                .Returns(new List<UserDM>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = await controller.GetUsers(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "GetUsers");
        }

        [TestMethod]
        public void BanUserRedirectToGetUsers()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            RedirectToRouteResult result = controller.BanUser(null) as RedirectToRouteResult;

            Assert.AreEqual("GetUsers", result.RouteValues["action"]);
        }

        [TestMethod]
        public void UnbanUserRedirectToGetUsers()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            RedirectToRouteResult result = controller.UnbanUser(null) as RedirectToRouteResult;

            Assert.AreEqual("GetUsers", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateCarViewEqualCreateCarCshtml()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.CreateCar() as ViewResult;

            Assert.AreEqual("CreateCar", result.ViewName);
        }

        [TestMethod]
        public void GetCarsViewResultNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            mockRentMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
                .Returns(new List<UserDM>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.GetCars(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public void GetCarsModelNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            mockRentMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
                .Returns(new List<UserDM>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.GetCars(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model,typeof(GetCarsVM));
        }

        [TestMethod]
        public void GetCarsViewResultEqualGetCarsCshtml()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            mockRentMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            mockIdentityMapper.Setup(x => x.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(new List<User>()))
                .Returns(new List<UserDM>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.GetCars(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual(result.ViewName, "GetCars");
        }

        [TestMethod]
        public void CreateCarViewRedirectToGetCars()
        {
            var model = new CreateVM()
            {
                Car = new CarDM() { DateOfCreate = DateTime.Now }
            };
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToCarDTO.Map<CarDM, CarDTO>(model.Car)).Returns(new CarDTO());
            mockRentMapper.Setup(x => x.ToImageDTO.Map<List<ImageDM>, IEnumerable<ImageDTO>>(new List<ImageDM>())).Returns(null as List<ImageDTO>);
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object)
            {
                ControllerContext=controllerContext.Object
            };

            RedirectToRouteResult result = controller.CreateCar(model) as RedirectToRouteResult;

            Assert.AreEqual("GetCars", result.RouteValues["action"]);
        }

        [TestMethod]
        public void UpdateCarModelCarNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCar(1)).Returns(null as CarDTO);
            mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.UpdateCar(1) as ViewResult;

            Assert.IsNotNull((result.Model as CreateVM).Car);
        }

        [TestMethod]
        public void UpdateCarViewResultEqualGetCarsCshtml()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCar(1)).Returns(null as CarDTO);
            mockRentMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.UpdateCar(1) as ViewResult;

            Assert.AreEqual(result.ViewName,"CreateCar");
        }

        [TestMethod]
        public void UpdateCarViewRedirectToGetCars()
        {
            var model = new CreateVM()
            {
                Car = new CarDM() { DateOfCreate = DateTime.Now }
            };
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            mockRentMapper.Setup(x => x.ToCarDTO.Map<CarDM, CarDTO>(model.Car)).Returns(new CarDTO());
            mockRentMapper.Setup(x => x.ToImageDTO.Map<List<ImageDM>, IEnumerable<ImageDTO>>(new List<ImageDM>())).Returns(null as List<ImageDTO>);
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };

            RedirectToRouteResult result = controller.UpdateCar(model) as RedirectToRouteResult;

            Assert.AreEqual("GetCars", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteViewRedirectToGetCars()
        {
            var model = new CreateVM()
            {
                Car = new CarDM() { DateOfCreate = DateTime.Now }
            };
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };

            RedirectToRouteResult result = controller.Delete(1) as RedirectToRouteResult;

            Assert.AreEqual("GetCars", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateManagerViewEqualRegisterCshtml()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            ViewResult result = controller.CreateManager() as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void CreateManagerViewRedirectToGetUsers()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Moq.Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("name");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            mockAdmin.Setup(x => x.CreateManager(null)).Returns("");
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object)
            {
                ControllerContext = controllerContext.Object
            };

            RedirectToRouteResult result = controller.CreateManager(new WEB.Models.View_Models.Account.RegisterVM()) as RedirectToRouteResult;

            Assert.AreEqual("GetUsers", result.RouteValues["action"]);
        }

        [TestMethod]
        public void AutocompleteBrandResultNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            JsonResult result = controller.AutocompleteBrand("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void AutocompleteCarcassResultNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            JsonResult result = controller.AutocompleteCarcass("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void AutocompleteQualityResultNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            JsonResult result = controller.AutocompleteQuality("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void AutocompleteTransmissionResultNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            JsonResult result = controller.AutocompleteTransmission("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void AutocompletePropertyNameNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            JsonResult result = controller.AutocompletePropertyName("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void AutocompletePropertyValueNotNull()
        {
            var mockAdmin = new Mock<IAdminService>();
            var mockRentMapper = new Mock<IRentMapperDM>();
            var mockIdentityMapper = new Mock<IIdentityMapperDM>();
            var mockRent = new Mock<IRentService>();
            var mockLog = new Mock<ILogWriter>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            AdminController controller = new AdminController(mockAdmin.Object, mockIdentityMapper.Object, mockRentMapper.Object,
                mockRent.Object, mockLog.Object);

            JsonResult result = controller.AutocompletePropertyValue("test") as JsonResult;

            Assert.IsNotNull(result.Data);
        }
    }
}
