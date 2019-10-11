using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Identity
{
    public class ProfileDTO
    {
        public string Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfExpiry { get; set; }

        public string Number { get; set; }

        public string Nationality { get; set; }

        public string Record { get; set; }

        public DateTime DateOfIssue { get; set; }

        public string RNTRC { get; set; }

        public string Authory { get; set; }

        public string PlaceOfBirth { get; set; }

        public User User { get; set; }
    }
}
