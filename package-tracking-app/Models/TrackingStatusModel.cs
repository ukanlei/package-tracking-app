using System;
using Shippo;

namespace package_tracking_app.Models
{
    public class TrackingStatusModel
    {
        public DateTime? ObjectCreated { get; set; }
        public DateTime? ObjectUpdated { get; set; }
        public ShippoEnums.TrackingStatus Status { get; set; }
        public string StatusDetails { get; set; }
        public DateTime? StatusDate { get; set; }
        public Substatus Substatus { get; set; }
        public ShortAddress Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }


        public TrackingStatusModel(ShippoEnums.TrackingStatus status, string city, string state, DateTime? statusDate)
        {
            Status = status;
            City = city;
            State = state;
            StatusDate = statusDate;
        }

        public TrackingStatusModel()
        {
        }
    }
}
