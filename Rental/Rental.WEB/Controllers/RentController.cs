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

        public ActionResult Index(IndexVM model)
        {

            var cars = _rentMapperDM.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(_rentService.GetCars());
            var filters = new List<Models.View_Models.Shared.Filter>();
            int? minPrice = 0,
                 maxPrice = 0,
                 minCurPrice = 0,
                 maxCurPrice = 0;
            if (cars != null && cars.Count > 0)
            {
                if (model.Filters == null || model.Filters.Count == 0)
                {
                    void CreateFilters(string name,Func<CarDM,string> value)
                    {
                        filters.AddRange(cars.Select(x => value(x)).Distinct()
                            .Select(x => new Models.View_Models.Shared.Filter() { Name = name, Text = x, Checked = false }));
                    }
                    CreateFilters("Марка", x => x.Brand.Name);
                    CreateFilters("Вместительность", x => x.Кoominess.ToString());
                    CreateFilters("Топливо", x => x.Fuel);
                    CreateFilters("Коробка", x => x.Transmission.Category);
                    CreateFilters("Кузов", x => x.Carcass.Type);
                    CreateFilters("Качество", x => x.Quality.Text);
                    maxCurPrice = maxPrice = cars.Max(x => x.Price) + 1;
                    minCurPrice = minPrice = cars.Min(x => x.Price) - 1;
                }
                else
                {
                    filters = model.Filters;
                    minCurPrice = model.CurrentPriceMin;
                    maxCurPrice = model.CurrentPriceMax;
                    minPrice = model.PriceMin;
                    maxPrice = model.PriceMax;

                    void FilterTest(string name, Func<CarDM, string> value)
                    {
                        if (cars.Count > 0 && model.Filters.Any(f => f.Name == name && f.Checked))
                        {
                            cars = cars.Where(p => model.Filters.Any(f => f.Name == name && f.Text == value(p) && f.Checked)).ToList();
                        }
                    }

                    FilterTest("Марка", x => x.Brand.Name);
                    FilterTest("Вместительность", x => x.Кoominess.ToString());
                    FilterTest("Топливо", x => x.Fuel);
                    FilterTest("Коробка", x => x.Transmission.Category);
                    FilterTest("Кузов", x => x.Carcass.Type);
                    FilterTest("Качество", x => x.Quality.Text);
                    cars = cars.Where(p => p.Price >= model.CurrentPriceMin && p.Price <= model.CurrentPriceMax).ToList();
                }
            }
            IndexVM indexVM = new IndexVM()
            {
                Cars = cars,
                CurrentPriceMax=maxCurPrice,
                CurrentPriceMin=minCurPrice,
                PriceMax=maxPrice,
                PriceMin=minPrice,
                Filters=filters
            };
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