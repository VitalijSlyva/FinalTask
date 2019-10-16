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

        public async Task<ActionResult> GetUsers()
        {
            var users = _adminService.GetUsers();
            var usersDM = _identityMapperDM.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(users);
            GetUsersVM getUsersVM = new GetUsersVM() {UsersDM=usersDM };
            var roles = new Dictionary<string, string>();
            var banns = new Dictionary<string, bool>();
            foreach(var i in users)
            {
                string role = (await _adminService.GetRolesAsync(i.Id)).Contains("manager")?"Менеджер":"Клиент";
                bool bann = (await _adminService.GetRolesAsync(i.Id)).Contains("banned");
                roles.Add(i.Id, role);
                banns.Add(i.Id, bann);

            }
            getUsersVM.Roles = roles;
            getUsersVM.Banns = banns;
            return View(getUsersVM);
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
            return View();
        }

        public ActionResult GetCars()
        {
            var cars = _rentService.GetCars();
            var carsDM = _rentMapperDM.ToCarDM.Map<IEnumerable<CarDTO>, List<CarDM>>(cars);
            GetCarsVM carsVM = new GetCarsVM() {  CarsDM = carsDM };
            return View(carsVM);
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
                return new HttpNotFoundResult();
            CarDM car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(_rentService.GetCar(id));
            return View("CreateCar", new CreateVM() { Car=car });

        }

        [HttpPost]
        public ActionResult UpdateCar(CreateVM model)
        {
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _createCarDTO(model);
            _adminService.UpdateCar(carDTO);
                _logWriter.CreateLog("Обновил данные про автомобиль " + model.Car.Id, User.Identity.GetUserId());
                return RedirectToAction("GetCars");
            }
            return View("Create", model );
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult();
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
                if (result.Length == 0)
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

        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            _adminService.Dispose();
            base.Dispose(disposing);
        }
    }
}