using System;
using System.ComponentModel.DataAnnotations;

namespace package_tracking_app.ViewModels
{
    public class AddPackageViewModel
    {
        [Required(ErrorMessage = "Tracking number required")]
        public string TrackingNumber { get; set; }

        public string Carrier { get; set; }

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        public AddPackageViewModel()
        {
        }
    }
}
