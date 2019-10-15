using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    [ExceptionLogger]
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
            var cars = _rentMapperDM.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(_rentService.GetCars());
            IndexVM indexVM = new IndexVM();
            indexVM.Cars = cars;
            return View(indexVM);
        }

        public ActionResult Car(int?id)
        {
            if (id != null)
            {
                var car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(_rentService.GetCar(id.Value));
                if (car == null)
                    return new HttpNotFoundResult();
                return View(car);
            }
            return new HttpNotFoundResult();
        }

        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            base.Dispose(disposing);
        }
    }
}