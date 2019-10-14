using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Rent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.EF.Initializers
{
    internal class RentInitializer: DropCreateDatabaseIfModelChanges<RentContext>
    {
        protected override void Seed(RentContext context)
        {
            var car = new Car() {
                Brand = new Brand() { Name = "B1" },
                Carcass = new Carcass() { Type = "C1" },
                Carrying = 6,
                DateOfCreate = DateTime.Now,
                Doors = 4,
                EngineVolume = 100,
                Fuel = "Gas",
                Hoursepower = 333,
                Number = "123",
                IsDeleted = false,
                Price=100,
                Кoominess=12,
                Transmission=new Transmission() {Category="Auto",Count=12 },
                Images=new List<Image>() { new Image() {Photo=null, Text="123"} },
                Model="Model 1 ",
                Quality=new Quality() {Text="Super" },
                Properties=new List<Property>() { new Property() {Name="p1",Text="v1" } }  
            };
            context.Cars.Add(car);
            base.Seed(context);
        }
    }
}
