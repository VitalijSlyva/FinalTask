using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
    public class CarDTO : EntityDTO
    {
        public string Model { set; get; }

        public BrandDTO Brand { get; set; }

        public string Number { get; set; }

        public int Price { get; set; }

        public int Doors { get; set; }

        public int Кoominess { get; set; }

        public string Fuel { get; set; }

        public int Carrying { get; set; }

        public double EngineVolume { get; set; }

        public double Hoursepower { get; set; }

        public DateTime DateOfCreate { get; set; }

        public TransmissionDTO Transmission { get; set; }

        public CarcassDTO Carcass { get; set; }

        public QualityDTO Quality { get; set; }

        public IEnumerable<PropertyDTO> Properties { get; set; }

        public IEnumerable<ImageDTO> Images { get; set; }

        public bool IsDeleted { get; set; }
    }
}
