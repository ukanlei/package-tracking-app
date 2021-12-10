using System;
using System.ComponentModel.DataAnnotations;

namespace package_tracking_app.Models
{
    public class Package
    {
        public int Id { get; set; } //primary key
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string Description { get; set; }

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
