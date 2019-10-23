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
    /// <summary>
    /// Testing rent controller
    /// </summary>
    [TestClass]
    public class RentControllerTest
    {
        private Mock<IRentService> _mockRent;
        private Mock<IRentMapperDM> _mockMapper;
        private RentController _controller;


        /// <summary>
        /// Initialization
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockRent = new Mock<IRentService>();
            _mockRent.Setup(x => x.GetCars()).Returns(new List<CarDTO>());
            _mockRent.Setup(x => x.GetCar(1)).Returns(new CarDTO());
            _mockMapper = new Mock<IRentMapperDM>();
            _mockMapper.Setup(x => x.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(new List<CarDTO>())).Returns(new List<CarDM>());
            _mockMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(new CarDTO())).Returns(new CarDM());
            _controller  = new RentController(_mockRent.Object, _mockMapper.Object);
        }


        /// <summary>
        /// The test that index view result not null
        /// </summary>
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            ViewResult result = _controller.Index(null, 0, 0, 1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that index view is 
        /// </summary>
        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            ViewResult result = _controller.Index(null, 0, 0, 1) as ViewResult;

            Assert.AreEqual("Index",result.ViewName);
        }

        /// <summary>
        /// The test that index model not null
        /// </summary>
        [TestMethod]
        public void IndexModelNotNull()
        {
            ViewResult result = _controller.Index(null, 0, 0, 1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(IndexVM));
        }

        /// <summary>
        /// The test that car view result not null
        /// </summary>
        [TestMethod]
        public void CarViewResultNotNull()
        {
            ViewResult result = _controller.Car(1) as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        /// <summary>
        /// The test that car view is index.cshtml
        /// </summary>
        [TestMethod]
        public void CarViewEqualIndexCshtml()
        {
            _mockRent.Setup(x => x.GetCar(1)).Returns(null as CarDTO);
            _mockMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM() { Id = 1 });

            ViewResult result = _controller.Car(1) as ViewResult;

            Assert.AreEqual("Car", result.ViewName);
        }

        /// <summary>
        /// The test that car model not null
        /// </summary>
        [TestMethod]
        public void CarModelNotNull()
        {
            _mockRent.Setup(x => x.GetCar(1)).Returns(null as CarDTO);
            _mockMapper.Setup(x => x.ToCarDM.Map<CarDTO, CarDM>(null)).Returns(new CarDM() { Id = 1 });

            ViewResult result = _controller.Car(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(CarDM));
        }
    }
}
