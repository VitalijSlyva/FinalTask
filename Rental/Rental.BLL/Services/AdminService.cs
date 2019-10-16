using Microsoft.AspNet.Identity;
using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Entities.Rent;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    public class AdminService : Service,  IAdminService
    {
        public AdminService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService log)
                : base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                List<ApplicationUser> users = IdentityUnitOfWork.UserManager.Users.ToList();
                users.RemoveAll(x => IdentityUnitOfWork.UserManager.IsInRole(x.Id, "admin"));
                return IdentityMapperDTO.ToUserDTO.Map<IEnumerable<ApplicationUser>, List<User>>(users);
            }
            catch(Exception e)
            {
                CreateLog(e, "AdminService", "GetUsers");
                return null;
            }
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string id)
        {
            try { 
            if (id == null)
                return null;
            var roles =await IdentityUnitOfWork.UserManager.GetRolesAsync(id);
            return roles;
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "GetRolesAsync");
                return null;
            }
        }

        public void BanUser(string userId)
        {
            try
            {
                var role = IdentityUnitOfWork.RoleManager.FindByName("banned");
                if (role == null)
                {
                    role = new ApplicationRole { Name = "banned" };
                    IdentityUnitOfWork.RoleManager.Create(role);
                }
                IdentityUnitOfWork.UserManager.AddToRole(userId, "banned");
                IdentityUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "BanUserAsync");
            }
        }

        public void CreateCar(CarDTO carDTO)
        {
            try
            {
                Car car = RentMapperDTO.ToCar.Map<CarDTO, Car>(carDTO);
                Brand brand = RentUnitOfWork.Brands.Find(x => x.Name.ToLower().Replace(" ", "") == carDTO.Brand.Name.ToLower().Replace(" ", ""))?.FirstOrDefault();
                if (brand == null)
                {
                    brand = new Brand() { Name = carDTO.Brand.Name.Trim() };
                    RentUnitOfWork.Brands.Create(brand);
                }
                car.Brand = brand;
                Transmission transmission = RentUnitOfWork.Transmissions.Find(x=>x.Category.ToLower().Replace(" ","")==carDTO.Transmission.Category.ToLower().Replace(" ", "")&&
                x.Count==carDTO.Transmission.Count)?.FirstOrDefault();
                if (transmission == null)
                {
                    transmission = new Transmission(){Category=carDTO.Transmission.Category,Count=carDTO.Transmission.Count};
                    RentUnitOfWork.Transmissions.Create(transmission);
                }
                car.Transmission = transmission;
                Carcass carcass = RentUnitOfWork.Carcasses.Find(x=>x.Type.ToLower().Replace(" ", "")==carDTO.Carcass.Type.ToLower().Replace(" ", ""))?.FirstOrDefault();
                if (carcass == null)
                {
                    carcass = new Carcass() {Type=carDTO.Carcass.Type };
                    RentUnitOfWork.Carcasses.Create(carcass);
                }
                car.Carcass = carcass;
                Quality quality = RentUnitOfWork.Qualities.Find(x=>x.Text.ToLower().Replace(" ", "")==carDTO.Quality.Text.ToLower().Replace(" ", ""))?.FirstOrDefault();
                if (quality == null)
                {
                    quality = new Quality() {Text=carDTO.Quality.Text };
                    RentUnitOfWork.Qualities.Create(quality);
                }
                car.Quality = quality;
                List<Property> properties = new List<Property>();
                foreach (var i in carDTO.Properties)
                {
                    Property property = new Property() { Name = i.Name, Text = i.Text };
                    RentUnitOfWork.Properties.Create(property);
                    properties.Add(property);
                }
                car.Properties = properties;
                List<Image> images = new List<Image>();
                foreach (var i in carDTO.Images)
                {
                    Image image = new Image() { Photo = i.Photo, Text = i.Text.ToLower().Trim() };
                    RentUnitOfWork.Images.Create(image);
                    images.Add(image);
                }
                car.Images = images;
                RentUnitOfWork.Cars.Create(car);
                RentUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "CreateCar");
            }
        }

        public string CreateManager(User userDTO)
        {
            try
            {
                ApplicationUser user = IdentityUnitOfWork.UserManager.FindByEmail(userDTO.Email);
                if (user == null)
                {
                    user = new ApplicationUser() { UserName = userDTO.Email, Email = userDTO.Email,Name=userDTO.Name };
                    var result = IdentityUnitOfWork.UserManager.Create(user, userDTO.Password);
                    if (result.Errors.Count() > 0)
                        return result.Errors.FirstOrDefault();
                    var role = IdentityUnitOfWork.RoleManager.FindByName("manager");
                    if (role == null)
                    {
                        role = new ApplicationRole { Name = "manager" };
                        IdentityUnitOfWork.RoleManager.Create(role);
                    }
                    IdentityUnitOfWork.UserManager.AddToRole(user.Id, "manager");
                    return "";
                }
                else
                {
                    return "Пользователь уже существует;";
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "CreateManager");
                return "Произошла ошибка";
            }

        }

        public void DeleteCar(int id)
        {
            try
            {
                Car car = RentUnitOfWork.Cars.Get(id);
                if (car != null)
                {
                    car.IsDeleted = true;
                    RentUnitOfWork.Cars.Update(car);
                    RentUnitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "DeleteCar");
            }
        }

        public void UnbanUser(string userId)
        {
            try
            {
                IdentityUnitOfWork.UserManager.RemoveFromRole(userId, "banned");
                IdentityUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "UnbanUserAsync");
            }
        }

        public void UpdateCar(CarDTO carDTO)
        {
            try
            {
                var oldCar = RentUnitOfWork.Cars.Get(carDTO.Id);
                var propert = oldCar.Properties.ToList();
                foreach (var i in propert)
                {
                    RentUnitOfWork.Properties.Delete(i.Id);
                }
                var imag = oldCar.Images.ToList();
                foreach (var i in imag)
                {
                    RentUnitOfWork.Images.Delete(i.Id);
                }
                oldCar.Properties.Clear();
                oldCar.Images.Clear();
                RentUnitOfWork.Save();
                if (carDTO.Brand.Name != oldCar.Brand.Name)
                {
                    Brand brand = RentUnitOfWork.Brands.Find(x => x.Name == carDTO.Brand.Name).FirstOrDefault();
                    if (brand == null) {
                        brand = new Brand()
                        {
                            Name =carDTO.Brand.Name
                        };
                    }
                    oldCar.Brand = brand ;
                }
                if (carDTO.Carcass.Type != oldCar.Carcass.Type)
                {
                    Carcass carcass= RentUnitOfWork.Carcasses.Find(x => x.Type == carDTO.Carcass.Type).FirstOrDefault();
                    if (carcass == null)
                    {
                        carcass = new Carcass()
                        {
                            Type=carDTO.Carcass.Type
                        };
                    }
                    oldCar.Carcass = carcass;
                }
                oldCar.Carrying = carDTO.Carrying;
                oldCar.DateOfCreate = oldCar.DateOfCreate;
                oldCar.Doors = carDTO.Doors;
                oldCar.EngineVolume = carDTO.EngineVolume;
                oldCar.Fuel = carDTO.Fuel;
                oldCar.Hoursepower = carDTO.Hoursepower;
                oldCar.Model = carDTO.Model;
                oldCar.Number = carDTO.Number;
                oldCar.Price = carDTO.Price;
                if (carDTO.Quality.Text != oldCar.Quality.Text)
                {
                    Quality quality = RentUnitOfWork.Qualities.Find(x => x.Text == carDTO.Quality.Text).FirstOrDefault();
                    if (quality == null)
                    {
                        quality = new Quality()
                        {
                            Text = carDTO.Quality.Text
                        };
                    }
                    oldCar.Quality = quality;
                }
                if (carDTO.Transmission.Category != oldCar.Transmission.Category|| carDTO.Transmission.Count != oldCar.Transmission.Count)
                {
                    Transmission transmission = RentUnitOfWork.Transmissions.Find(x => carDTO.Transmission.Category == x.Category && carDTO.Transmission.Count == x.Count)
                        .FirstOrDefault();
                    if (transmission == null)
                    {
                        transmission = new Transmission()
                        {
                            Count= carDTO.Transmission.Count,
                            Category=carDTO.Transmission.Category
                        };
                    }
                    oldCar.Transmission= transmission;
                }
                oldCar.Кoominess = carDTO.Кoominess;

                List<Property> properties = new List<Property>();
                foreach (var i in carDTO.Properties)
                {
                    Property property = new Property() { Name = i.Name, Text = i.Text };
                    RentUnitOfWork.Properties.Create(property);
                    properties.Add(property);
                }
                oldCar.Properties = properties;
                List<Image> images = new List<Image>();
                foreach (var i in carDTO.Images)
                {
                    Image image = new Image() { Photo = i.Photo, Text = i.Text.ToLower().Trim() };
                    RentUnitOfWork.Images.Create(image);
                    images.Add(image);
                }
                oldCar.Images = images;
                RentUnitOfWork.Cars.Update(oldCar);
                RentUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "UpdateCar");
            }
        }
    }
}
