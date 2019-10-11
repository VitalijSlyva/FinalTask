using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Identity
{
    public class ProfileDM
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

        public UserDM User { get; set; }
    }
}