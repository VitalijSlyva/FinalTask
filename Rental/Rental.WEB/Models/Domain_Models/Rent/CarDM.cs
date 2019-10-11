using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class CarDM : EntityDM
    {
        public string Model { set; get; }

        public BrandDM Brand { get; set; }

        public string Number { get; set; }

        public int Price { get; set; }

        public int Doors { get; set; }

        public int Кoominess { get; set; }

        public string Fuel { get; set; }

        public int Carrying { get; set; }

        public double EngineVolume { get; set; }

        public double Hoursepower { get; set; }

        public DateTime DateOfCreate { get; set; }

        public TransmissionDM Transmission { get; set; }

        public CarcassDM Carcass { get; set; }

        public QualityDM Quality { get; set; }

        public List<PropertyDM> Properties { get; set; }

        public List<ImageDM> Images { get; set; }

        public bool IsDeleted { get; set; }
    }
}