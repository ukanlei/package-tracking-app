using System.Collections.Generic;
using Shippo;
namespace package_tracking_app.Models
{
    public class LocationModel
    {
        public ShortAddress Location { get; set; }

        public LocationModel(ShortAddress location)
        {
            Location = location;

        }
    }
}
