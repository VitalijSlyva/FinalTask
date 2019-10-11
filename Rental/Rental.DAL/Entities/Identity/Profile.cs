using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Identity
{
    public class Profile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
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

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
