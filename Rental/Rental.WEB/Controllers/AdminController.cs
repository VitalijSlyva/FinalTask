using Microsoft.AspNet.Identity;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Log;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Account;
using Rental.WEB.Models.View_Models.Admin;
using Rental.WEB.Models.View_Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private IAdminService _adminService;

        private IRentService _rentService;

        private IIdentityMapperDM _identityMapperDM;

        private IRentMapperDM _rentMapperDM;

        private ILogWriter _logWriter;

        public AdminController(IAdminService adminService, IIdentityMapperDM identityMapperDM,
            IRentMapperDM rentMapperDM,IRentService rentService,ILogWriter log)
        {
            _adminService = adminService;
            _identityMapperDM = identityMapperDM;
            _rentMapperDM = rentMapperDM;
            _rentService = rentService;
            _logWriter = log;
        }

        public async Task<ActionResult> GetUsers(GetUsersVM model, int sortMode = 0, int page = 1, int selectedMode = 1)
        {
            if (sortMode == 0)
            {
                sortMode = selectedMode;
            }
            var users = _adminService.GetUsers();
            var usersDM = _identityMapperDM.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(users);
            var roles = new Dictionary<string, string>();
            var banns = new Dictionary<string, bool>();
            foreach(var i in users)
            {
                string role = (await _adminService.GetRolesAsync(i.Id)).Contains("manager")?"Менеджер":"Клиент";
                bool bann = (await _adminService.GetRolesAsync(i.Id)).Contains("banned");
                roles.Add(i.Id, role);
                banns.Add(i.Id, bann);

            }
            var filters = new List<Models.View_Models.Shared.Filter>();
            if (usersDM != null && usersDM.Count > 0)
            {
                if (model.Filters == null || model.Filters.Count == 0)
                {
                    filters.AddRange(roles.Select(x => x.Value).Distinct()
                        .Select(x => new Models.View_Models.Shared.Filter() { Name = "Должность", Checked = false, Text = x }));

                    filters.AddRange(banns.Select(x => x.Value).Distinct()
                        .Select(x => new Models.View_Models.Shared.Filter() { Name = "Статус", Checked = false, Text = x?"Забанен":"Незабанен" }));
                }
                else
                {
                    filters = model.Filters;

                    if (usersDM.Count > 0 && model.Filters.Any(f => f.Name == "Должность" && f.Checked))
                    {
                        usersDM = usersDM.Where(p => model.Filters.Any(f => f.Name =="Должность" && f.Text == roles[p.Id] && f.Checked)).ToList();
                    }
                    if (usersDM.Count > 0 && model.Filters.Any(f => f.Name == "Статус" && f.Checked))
                    {
                        usersDM = usersDM.Where(p => model.Filters.Any(f => f.Name == "Статус" && (f.Text== "Забанен") == banns[p.Id] && f.Checked)).ToList();
                    }
                }
            }

            var sortModes = new List<string>();
            sortModes.Add("По должности");
            sortModes.Add("По должности");
            sortModes.Add("По бану");
            sortModes.Add("По бану");
            switch (sortMode)
            {
                case 1:
                    usersDM = usersDM.OrderBy(x =>roles[x.Id]).ToList();
                    break;
                case 2:
                    usersDM = usersDM.OrderByDescending(x => roles[x.Id]).ToList();
                    break;
                case 3:
                    usersDM = usersDM.OrderBy(x => banns[x.Id]).ToList();
                    break;
                case 4:
                    usersDM = usersDM.OrderByDescending(x => banns[x.Id]).ToList();
                    break;
            }

            int pageSize = 2;
            int count = usersDM.Count;
            usersDM = usersDM.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItem = count };
            if ((page < 1&&usersDM.Count>0) || page > pageInfo.TotalPages || sortMode < 1 || sortMode > sortModes.Count)
            {
                 return  View("CustomNotFound", "_Layout", "Страница не найдена");
            }
            GetUsersVM getUsersVM = new GetUsersVM() {
                UsersDM = usersDM,
                Filters = filters,
                Roles=roles,
                Banns=banns,
                PageInfo=pageInfo,
                SortModes=sortModes,
                SelectedMode=sortMode
            };
            return View("GetUsers",getUsersVM);
        }

        public ActionResult BanUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                _adminService.BanUser(id);
                _logWriter.CreateLog("Забанил пользователя "+id, User.Identity.GetUserId());
            }
            return RedirectToAction("GetUsers");
        }

        public ActionResult UnbanUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                _adminService.UnbanUser(id);
                _logWriter.CreateLog("Разбанил пользователя " + id, User.Identity.GetUserId());
            }
            return RedirectToAction("GetUsers");
        }

        public ActionResult CreateCar()
        {
            return View("CreateCar");
        }

        public ActionResult GetCars(GetCarsVM model, int sortMode = 0, int page = 1, int selectedMode = 1)
        {
            if (sortMode == 0)
            {
                sortMode = selectedMode;
            }
            var carsDTO = _rentService.GetCars();
            var cars = _rentMapperDM.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(carsDTO);
            var filters = new List<Models.View_Models.Shared.Filter>();
            int? minPrice = 0,
                 maxPrice = 0,
                 minCurPrice = 0,
                 maxCurPrice = 0;
            if (cars != null && cars.Count > 0)
            {
                if (model.Filters == null || model.Filters.Count == 0)
                {
                    void CreateFilters(string name, Func<CarDM, string> value)
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

            int pageSize = 2;
            int count = cars.Count;
            cars = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItem = count };
            if ((page < 1&&cars.Count!=0) || page > pageInfo.TotalPages || sortMode < 1 || sortMode > sortModes.Count)
            {
                 return  View("CustomNotFound", "_Layout", "Страница не найдена");
            }
            GetCarsVM carsVM = new GetCarsVM()
            {
                CarsDM = cars,
                CurrentPriceMax = maxCurPrice,
                CurrentPriceMin = minCurPrice,
                PriceMax = maxPrice,
                PriceMin = minPrice,
                Filters = filters,
                PageInfo=pageInfo,
                SortModes = sortModes,
                SelectedMode = sortMode
            };
            return View("GetCars",carsVM);
        }

        private CarDTO _createCarDTO(CreateVM model)
        {
            CarDTO carDTO = _rentMapperDM.ToCarDTO.Map<CarDM, CarDTO>(model.Car);
            List<PropertyDTO> properties = new List<PropertyDTO>();
            if(model.PropertyNames!=null&&model.PropertyNames.Length>0)
            for (int i = 0; i < model.PropertyNames.Count(); i++)
                properties.Add(new PropertyDTO() { Name = model.PropertyNames[i], Text = model.PropertyValues[i] });
            carDTO.Properties = properties;

            List<ImageDM> images = new List<ImageDM>();
            int ofset = 0;
            if (model.Photos != null && model.Photos.Length > 0)
            {
                ofset = model.Photos.Length;
                for (int i = 0; i < model.Photos.Length; i++)
                {
                    images.Add(new ImageDM { Text = model.Alts[i], Photo = _rentService.GetCar(model.Car.Id).Images.First(x => x.Id.ToString() == model.Photos[i]).Photo });
                }
            }
            if (model.Images != null)
            {
                for (int i = 0; i < model.Images.Length; i++)
                    using (var reader = new BinaryReader(model.Images[i].InputStream))
                        images.Add(new ImageDM { Photo = reader.ReadBytes(model.Images[i].ContentLength), Text = model.Alts[i + ofset] });
            }
            carDTO.Images = _rentMapperDM.ToImageDTO.Map<List<ImageDM>, IEnumerable<ImageDTO>>(images);
            return carDTO;
        }

        [HttpPost]
        public ActionResult CreateCar(CreateVM model)
        {
            if (model.Car.DateOfCreate.Date > DateTime.Now.Date ||
                model.Car.DateOfCreate.Date < DateTime.Now.AddYears(-30).Date)
                ModelState.AddModelError("DateOfCreate", "Неверная дата");
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _createCarDTO(model);
                _adminService.CreateCar(carDTO);
                _logWriter.CreateLog("Добавил автомобиль " , User.Identity.GetUserId());
                return RedirectToAction("GetCars");
            }
            return View(model);
        }

        public ActionResult UpdateCar(int? id)
        {
            if (id == null)
                return View("CustomNotFound", "_Layout", "Автомобиль не найден");
            var carDTO = _rentService.GetCar(id);
            CarDM car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(carDTO);
            return View("CreateCar", new CreateVM() { Car=car });

        }

        [HttpPost]
        public ActionResult UpdateCar(CreateVM model)
        {
            if (model.Car.DateOfCreate.Date > DateTime.Now.Date ||
                   model.Car.DateOfCreate.Date < DateTime.Now.AddYears(-30).Date)
                ModelState.AddModelError("Car.DateOfCreate", "Неверная дата");
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _createCarDTO(model);
            _adminService.UpdateCar(carDTO);
                _logWriter.CreateLog("Обновил данные про автомобиль " + model.Car.Id, User.Identity.GetUserId());
                return RedirectToAction("GetCars");
            }
            CarDM car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(_rentService.GetCar(model.Car.Id));
            model.Car.Properties = car.Properties;
            model.Car.Images = car.Images;
            return View("CreateCar", model );
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return View("CustomNotFound", "_Layout", "Автомобиль не найден");
            _adminService.DeleteCar(id.Value);
            _logWriter.CreateLog("Убрал автомобиль " + id, User.Identity.GetUserId());
            return RedirectToAction("GetCars");
        }

        public ActionResult CreateManager()
        {
            ViewBag.CreatingManger = true;
            return View("Register");
        }

        [HttpPost]
        public ActionResult CreateManager(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = register.Email,
                    Password = register.Password,
                    Name = register.Name,
                };
                string result = _adminService.CreateManager(user);
                if (result==null|| result.Length == 0)
                {
                    _logWriter.CreateLog("Добавил менеджера", User.Identity.GetUserId());
                    return RedirectToAction("GetUsers");
                }
                else
                    ModelState.AddModelError("", result);
            }
            ViewBag.CreatingManger = true;
            return View("Register", register);
        }

        public ActionResult AutocompleteBrand(string term)
        {
            if (term != null)
            {
                term = term.ToLower().Replace(" ", "");
                var items = _rentService.GetCars().Select(x=>x.Brand)
                    .Where(x => x.Name.ToLower().Replace(" ", "").Contains(term)).Select(x => x.Name.ToLower()).Distinct()
                    .Take(5).Select(x => new { value = x });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteCarcass(string term)
        {
            if (term != null)
            {
                term = term.ToLower().Replace(" ", "");
                var items = _rentService.GetCars().Select(x => x.Carcass)
                    .Where(x => x.Type.ToLower().Replace(" ", "").Contains(term)).Select(x => x.Type.ToLower()).Distinct()
                    .Take(5).Select(x => new { value = x });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteQuality(string term)
        {
            if (term != null)
            {
                term = term.ToLower().Replace(" ", "");
                var items = _rentService.GetCars().Select(x => x.Quality)
                    .Where(x => x.Text.ToLower().Replace(" ", "").Contains(term)).Select(x => x.Text.ToLower()).Distinct()
                    .Take(5).Select(x => new { value = x });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteTransmission(string term)
        {
            if (term != null)
            {
                term = term.ToLower().Replace(" ", "");
                var items = _rentService.GetCars().Select(x => x.Transmission)
                    .Where(x => x.Category.ToLower().Replace(" ", "").Contains(term)).Select(x => x.Category.ToLower()).Distinct()
                    .Take(5).Select(x => new { value = x });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompletePropertyName(string term)
        {
            if (term != null)
            {
                term = term.ToLower().Replace(" ", "");
                var properties = new List<PropertyDTO>();
                foreach(var i in _rentService.GetCars().Select(x => x.Properties))
                {
                    properties.AddRange(i);
                }
                var items = properties.Where(x => x.Name.ToLower().Replace(" ", "").Contains(term))
                    .Select(x => x.Name.ToLower()).Distinct().Take(5).Select(x => new { value = x });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompletePropertyValue(string term)
        {
            if (term != null)
            {
                term = term.ToLower().Replace(" ", "");
                var properties = new List<PropertyDTO>();
                foreach (var i in _rentService.GetCars().Select(x => x.Properties))
                {
                    properties.AddRange(i);
                }
                var items = properties.Where(x => x.Text.ToLower().Replace(" ", "").Contains(term))
                    .Select(x => x.Text.ToLower()).Distinct().Take(5).Select(x => new { value = x });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            _adminService.Dispose();
            base.Dispose(disposing);
        }
    }
}