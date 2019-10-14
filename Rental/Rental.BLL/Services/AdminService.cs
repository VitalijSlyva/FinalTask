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
                List<ApplicationUser> users = IdentityUnitOfWork.UserManager.Users.Where(x => !(IdentityUnitOfWork.UserManager.IsInRole(x.Id, "admin"))).ToList();
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

        public async Task BanUserAsync(string userId)
        {
            try
            {
                await IdentityUnitOfWork.UserManager.AddToRoleAsync(userId, "banned");
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

        public async void CreateManager(User userDTO)
        {
            try
            {
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindByEmailAsync(userDTO.Email);
                if (user == null)
                {
                    user = new ApplicationUser() { UserName = userDTO.Name, Email = userDTO.Email };
                    var result = await IdentityUnitOfWork.UserManager.CreateAsync(user, userDTO.Password);
                    if (result.Errors.Count() > 0)
                        throw new Exception();
                    var role = await IdentityUnitOfWork.RoleManager.FindByNameAsync("manager");
                    if (role == null)
                    {
                        role = new ApplicationRole { Name = "manager" };
                        await IdentityUnitOfWork.RoleManager.CreateAsync(role);
                    }
                    await IdentityUnitOfWork.UserManager.AddToRoleAsync(user.Id, "manager");
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "CreateManager");
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

        public async Task UnbanUserAsync(string userId)
        {
            try
            {
                await IdentityUnitOfWork.UserManager.RemoveFromRoleAsync(userId, "banned");
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
                RentUnitOfWork.Save();
                Car car = RentMapperDTO.ToCar.Map<CarDTO, Car>(carDTO);
                //Brand brand = RentUnitOfWork.Brands.Find(x => x.Name.ToLower().Replace(" ", "") == carDTO.Brand.Name.ToLower().Replace(" ", ""))?.FirstOrDefault();

                //car.Brand = brand;
                //Transmission transmission = RentUnitOfWork.Transmissions.Find(x => x.Category.ToLower().Replace(" ", "") == carDTO.Transmission.Category.ToLower().Replace(" ", "") &&
                //x.Count == carDTO.Transmission.Count)?.FirstOrDefault();
                //if (transmission == null)
                //{
                //    transmission = new Transmission() { Category = carDTO.Transmission.Category, Count = carDTO.Transmission.Count };
                //    RentUnitOfWork.Transmissions.Create(transmission);
                //}
                //car.Transmission = transmission;
                //Carcass carcass = RentUnitOfWork.Carcasses.Find(x => x.Type.ToLower().Replace(" ", "") == carDTO.Carcass.Type.ToLower().Replace(" ", ""))?.FirstOrDefault();
                //if (carcass == null)
                //{
                //    carcass = new Carcass() { Type = carDTO.Carcass.Type };
                //    RentUnitOfWork.Carcasses.Create(carcass);
                //}
                //car.Carcass = carcass;
                //Quality quality = RentUnitOfWork.Qualities.Find(x => x.Text.ToLower().Replace(" ", "") == carDTO.Quality.Text.ToLower().Replace(" ", ""))?.FirstOrDefault();
                //if (quality == null)
                //{
                //    quality = new Quality() { Text = carDTO.Quality.Text };
                //    RentUnitOfWork.Qualities.Create(quality);
                //}
                //car.Quality = quality;
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
                RentUnitOfWork.Cars.Update(car);
                RentUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "AdminService", "UpdateCar");
            }
        }
    }
}
