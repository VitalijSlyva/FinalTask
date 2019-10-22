using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Controllers;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Rent;

namespace Rental.Tests
{
    [TestClass]
    public class RentControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            var mockRent = new Mock<IRentService>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            var mockMapper = new Mock<IRentMapperDM>();
            mockMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            RentController controller = new RentController(mockRent.Object, mockMapper.Object);

            ViewResult result = controller.Index(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            var mockRent = new Mock<IRentService>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            var mockMapper = new Mock<IRentMapperDM>();
            mockMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            RentController controller = new RentController(mockRent.Object, mockMapper.Object);

            ViewResult result = controller.Index(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual("Index",result.ViewName);
        }

        [TestMethod]
        public void IndexModelNotNull()
        {
            var mockRent = new Mock<IRentService>();
            mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            var mockMapper = new Mock<IRentMapperDM>();
            mockMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            RentController controller = new RentController(mockRent.Object, mockMapper.Object);

            ViewResult result = controller.Index(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(IndexVM));
        }

        [TestMethod]
        public void CarViewResultNotNull()
        {
            var mockRent = new Mock<IRentService>();
            mockRent.Setup(x => x.GetCar(1)).Returns(new CarDTO());
            var mockMapper = new Mock<IRentMapperDM>();
            mockMapper.Setup(x => x.ToCarDM.Map<CarDTO,CarDM>(new CarDTO())).Returns(new CarDM());
            RentController controller = new RentController(mockRent.Object, mockMapper.Object);

            ViewResult result = controller.Car(1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public void CarViewEqualIndexCshtml()
        {
            var mockRent = new Mock<IRentService>();
            mockRent.Setup(x => x.GetCar(1)).Returns(null as CarDTO);
            var mockMapper = new Mock<IRentMapperDM>();
            mockMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM() {Id=1 });
            RentController controller = new RentController(mockRent.Object, mockMapper.Object);

            ViewResult result = controller.Car(1) as ViewResult;

            Assert.AreEqual("Car", result.ViewName);
        }

        [TestMethod]
        public void CarModelNotNull()
        {
            var mockRent = new Mock<IRentService>();
            mockRent.Setup(x => x.GetCar(1)).Returns(null as CarDTO);
            var mockMapper = new Mock<IRentMapperDM>();
            mockMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM() { Id = 1 });
            RentController controller = new RentController(mockRent.Object, mockMapper.Object);

            ViewResult result = controller.Car(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(CarDM));
        }
    }
}
