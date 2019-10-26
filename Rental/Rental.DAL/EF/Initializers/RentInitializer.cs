using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Rent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace Rental.DAL.EF.Initializers
{
    /// <summary>
    /// Intializator for rent context.
    /// </summary>
    internal class RentInitializer : CreateDatabaseIfNotExists<RentContext>
    {
        /// <summary>
        /// Recreate database with new items.
        /// </summary>
        /// <param name="context">Database context</param>
        protected override void Seed(RentContext context)
        {
            var car = new Car()
            {
                Brand = new Brand() { Name = "ford" },
                Carcass = new Carcass() { Type = "седан" },
                Carrying = 1000,
                DateOfCreate = DateTime.Now.AddYears(-5),
                Doors = 2,
                EngineVolume = 5,
                Fuel = "бензин",
                Hoursepower = 333,
                Number = "XY1234YX",
                IsDeleted = false,
                Price = 500,
                Кoominess = 4,
                Transmission = new Transmission() { Category = "автомат", Count = 6 },
                Images = new List<Image>() {
                    new Image() {
                    Photo = File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\EF\Initializers\Images\1.jpg")
                    .Replace("WEB","DAL")),
                    Text ="фото"
                }
                },
                Model = "mustang gtr",
                Quality = new Quality() { Text = "c" },
                Properties = new List<Property>() { new Property() { Name = "бронрованные стекла", Text = "да" } }
            };

            var car2 = new Car()
            {
                Brand = car.Brand,
                Carcass = car.Carcass,
                Carrying = 2000,
                DateOfCreate = DateTime.Now.AddYears(-3),
                Doors = 4,
                EngineVolume = 6,
                Fuel = "бензин",
                Hoursepower = 533,
                Number = "XQ1224QX",
                IsDeleted = false,
                Price = 700,
                Кoominess = 5,
                Transmission = new Transmission() { Category = "автомат", Count = 10 },
                Images = new List<Image>() {
                    new Image() {
                    Photo = File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\EF\Initializers\Images\2.jpg")
                    .Replace("WEB","DAL")),
                    Text ="фото"
                }
                },
                Model = "mustang gtx 530",
                Quality = new Quality() { Text = "b" },
                Properties = new List<Property>() { }
            };

            var car3 = new Car()
            {
                Brand = new Brand() { Name = "lada" },
                Carcass = car.Carcass,
                Carrying = 1500,
                DateOfCreate = DateTime.Now.AddYears(-10),
                Doors = 4,
                EngineVolume = 3,
                Fuel = "газ",
                Hoursepower = 230,
                Number = "XQ1111QX",
                IsDeleted = false,
                Price = 300,
                Кoominess = 5,
                Transmission = new Transmission() { Category = "механика", Count = 5 },
                Images = new List<Image>() {
                    new Image() {
                    Photo = File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\EF\Initializers\Images\3.jpg")
                    .Replace("WEB","DAL")),
                    Text ="фото"
                }
                },
                Model = "10",
                Quality = new Quality() { Text = "a" },
                Properties = new List<Property>() { }
            };

            context.Cars.Add(car);
            context.Cars.Add(car2);
            context.Cars.Add(car3);

            base.Seed(context);
        }
    }
}
