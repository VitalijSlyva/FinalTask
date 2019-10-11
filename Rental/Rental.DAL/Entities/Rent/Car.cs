using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Car : Entity
    {
        public string Model { set; get; }

        public virtual Brand Brand { get; set; }

        public int? BrandId { get; set; }

        public string Number { get; set; }

        public int Price { get; set; }

        public int Doors { get; set; }

        public int Кoominess { get; set; }

        public string Fuel { get; set; }

        public int Carrying { get; set; }

        public double EngineVolume { get; set; }

        public double Hoursepower { get; set; }

        public DateTime DateOfCreate { get; set; }

        public int? TransmissionId { get; set; }

        public virtual Transmission Transmission { get; set; }

        public virtual Carcass Carcass{ get; set; }

        public int? CarcassId { get; set; }

        public virtual Quality Quality { get; set; }

        public int? QualityId { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public bool IsDeleted { get; set; }
    }
}
