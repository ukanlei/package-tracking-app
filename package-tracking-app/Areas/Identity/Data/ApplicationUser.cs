using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using package_tracking_app.Models;

namespace package_tracking_app.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //allow u to add multiple package entities to a user
        public virtual ICollection<Package> Packages{ get; set; }

        public ApplicationUser()
        {

        }
    }
}
