using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using package_tracking_app.Areas.Identity.Data;

namespace package_tracking_app.Models
{
    public class Package
    {
        public int Id { get; set; } //primary key
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string Description { get; set; }

        public int ApplicationUserId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Package()
        {
  
        }

        public Package(string trackingNumber, string carrier)
        {
            TrackingNumber = trackingNumber;
            Carrier = carrier;
        }

      
    }
}
