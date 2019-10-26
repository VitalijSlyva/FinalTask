using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rental.WEB.Models.View_Models.Shared;
using System.Net.Mail;

namespace Rental.WEB.Controllers
{
    /// <summary>
    /// Controller with base actions.
    /// </summary>
    [ExceptionLogger]
    public class RentController : Controller
    {
        private IRentService _rentService;

        private IRentMapperDM _rentMapperDM;
            
        /// <summary>
        /// Create services and mappers for work.
        /// </summary>
        /// <param name="rentService">Rent service</param>
        /// <param name="rentMapper">Rent mapper</param>
        public RentController(IRentService rentService, IRentMapperDM rentMapper)
        {
            _rentService = rentService;
            _rentMapperDM = rentMapper;
        }

        /// <summary>
        /// Show all cars.
        /// </summary>
        /// <param name="model">View model</param>
        /// <param name="sortMode">Sort mode</param>
        /// <param name="page">Page number</param>
        /// <param name="selectedMode">Selected sort mode</param>
        /// <returns>View</returns>
        public ActionResult Index(IndexVM model, int sortMode = 0, int page = 1, int selectedMode = 1)
        {
            if (sortMode == 0)
            {
                sortMode = selectedMode;
            }

            var cars = _rentMapperDM.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(_rentService.GetCars());
            var filters = new List<Models.View_Models.Shared.Filter>();
            int? minPrice = 0,
                 maxPrice = 0,
                 minCurPrice = 0,
                 maxCurPrice = 0;

            if (cars != null && cars.Count > 0)
            {
                if (model?.Filters == null || model?.Filters?.Count == 0)
                {
                    void CreateFilters(string name, Func<CarDM, string> value)
                    {
                        filters.AddRange(cars.Select(x => value(x)).Distinct()
                            .Select(x => new Models.View_Models.Shared.Filter()
                            {
                                Name = name,
                                Text = x,
                                Checked = false
                            }));
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
                            cars = cars.Where(p => model.Filters.Any(f => f.Name == name && f.Text == value(p) && f.Checked))
                                .ToList();
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

            var sortModes = new List<string>();
            sortModes.Add("По марке");
            sortModes.Add("По марке");
            sortModes.Add("По цене");
            sortModes.Add("По цене");
            sortModes.Add("По объему двигателя");
            sortModes.Add("По объему двигателя");
            sortModes.Add("По вместительности");
            sortModes.Add("По вместительности");

            switch (sortMode)
            {
                case 1:
                    cars = cars.OrderBy(x => x.Brand.Name).ToList();
                    break;
                case 2:
                    cars = cars.OrderByDescending(x => x.Brand.Name).ToList();
                    break;
                case 3:
                    cars = cars.OrderBy(x => x.Price).ToList();
                    break;
                case 4:
                    cars = cars.OrderByDescending(x => x.Price).ToList();
                    break;
                case 5:
                    cars = cars.OrderBy(x => x.EngineVolume).ToList();
                    break;
                case 6:
                    cars = cars.OrderByDescending(x => x.EngineVolume).ToList();
                    break;
                case 7:
                    cars = cars.OrderBy(x => x.Кoominess).ToList();
                    break;
                case 8:
                    cars = cars.OrderByDescending(x => x.Кoominess).ToList();
                    break;
            }

            int pageSize = 5;
            int count = cars.Count;
            cars = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItem = count };
            if ((page < 1 &&count!=0) || (page > pageInfo.TotalPages && count != 0)
                || sortMode<1||sortMode>sortModes.Count)
            {
               return  View("CustomNotFound", "_Layout", "Страница не найдена");
            } 

            IndexVM indexVM = new IndexVM()
            {
                Cars = cars,
                CurrentPriceMax = maxCurPrice,
                CurrentPriceMin = minCurPrice,
                PriceMax = maxPrice,
                PriceMin = minPrice,
                Filters = filters,
                PageInfo = pageInfo,
                SortModes = sortModes,
                SelectedMode = sortMode
            };

            return View("Index",indexVM);
        }

        /// <summary>
        /// Show car by id.
        /// </summary>
        /// <param name="id">Car id.</param>
        /// <returns>View</returns>
        public ActionResult Car(int?id)
        {
            if (id != null)
            {
                var carDTO = _rentService.GetCar(id.Value);
                var car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(carDTO);
                if (car == null)
                    return View("CustomNotFound", "_Layout", "Автомобиль не найден");
                ViewBag.FreeDays = _rentService.GetFreeDates(id.Value);
                return View("Car",car);
            }

            return View("CustomNotFound", "_Layout", "Автомобиль не найден");
        }

        /// <summary>
        /// Dispose services.
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            base.Dispose(disposing);
        }
    }
}