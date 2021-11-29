using System;
namespace package_tracking_app.Models
{
    public class Package
    {
        public int Id { get; set; } //primary key
        public int TrackingNumber { get; set; }
        public string Carrier { get; set; }

        public Package()
        {
  
        }

        public Package(int trackingNumber, string carrier)
        {
            TrackingNumber = trackingNumber;
            Carrier = carrier;
        }
    }
}
