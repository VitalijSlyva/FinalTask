using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    public class RentController : Controller
    {
        private IRentService _rentService;

        private IRentMapperDM _rentMapperDM;
            
        public RentController(IRentService rentService, IRentMapperDM rentMapper)
        {
            _rentService = rentService;
            _rentMapperDM = rentMapper;
        }

        public ActionResult Index()
        {
            var car = _rentMapperDM.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(_rentService.GetCars());
            return View();
        }

        public ActionResult Car()
        {
            var car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(_rentService.GetCar(1));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}