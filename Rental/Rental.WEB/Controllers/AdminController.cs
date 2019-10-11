using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
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
    public class AdminController : Controller
    {
        private IAdminService _adminService;

        private IRentService _rentService;

        private IIdentityMapperDM _identityMapperDM;

        private IRentMapperDM _rentMapperDM;

        public AdminController(IAdminService adminService, IIdentityMapperDM identityMapperDM,IRentMapperDM rentMapperDM,IRentService rentService)
        {
            _adminService = adminService;
            _identityMapperDM = identityMapperDM;
            _rentMapperDM = rentMapperDM;
            _rentService = rentService;
        }

        public ActionResult GetUsers()
        {
            var users = _adminService.GetUsers();
            var usersDM = _identityMapperDM.ToUserDM.Map<IEnumerable<User>, List<UserDM>>(users);
            GetUsersVM getUsersVM = new GetUsersVM() {UsersDM=usersDM };
            return View(usersDM);
        }

        public ActionResult BanUser(string id)
        {
            if (id != null)
            {
                _adminService.BanUserAsync(id);
            }
            return RedirectToAction("GetUsers");
        }

        public ActionResult UnBanUser(string id)
        {
            if (id != null)
            {
                _adminService.UnbanUserAsync(id);
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
            var carsDM = _identityMapperDM.ToUserDM.Map<IEnumerable<CarDTO>, List<CarDM>>(cars);
            GetCarsVM carsVM = new GetCarsVM() {  CarsDM = carsDM };
            return View(carsVM);
        }

        private CarDTO _createCarDTO(CreateVM model)
        {
            CarDTO carDTO = _rentMapperDM.ToCarDTO.Map<CarDM, CarDTO>(model.Car);
            List<PropertyDTO> properties = new List<PropertyDTO>();
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
            CarDTO carDTO = _createCarDTO(model);
            _adminService.CreateCar(carDTO);
            return RedirectToAction("GetCars");
        }
    
        public ActionResult UpdateCar(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult();
            CarDM car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(_rentService.GetCar(id));
            return View("Create", new CreateVM() { Car=car });

        }

        [HttpPost]
        public ActionResult UpdateCar(CreateVM model)
        {
            CarDTO carDTO = _createCarDTO(model);
            _adminService.UpdateCar(carDTO);
            return RedirectToAction("GetCars");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult();
            _adminService.DeleteCar(id.Value);
            return RedirectToAction("GatCars");
        }

        public ActionResult CreateManager()
        {
            return View();
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
                _adminService.CreateManager(user);
                return RedirectToAction("GetUsers");
            }
            return View(register);
        }
    }
}