using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Identity
{
    public class UserDM
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool Banned { get; set; }

        //     public string Role { get; set; }
    }
}