using System;
using Microsoft.AspNetCore.Identity;

namespace package_tracking_app.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ApplicationUser()
        {

        }
    }
}
