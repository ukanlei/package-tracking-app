using System.Collections.Generic;
using Shippo;
namespace package_tracking_app.Models
{
    public class TrackingHistoryModel

    {
        public List<TrackingHistory> Checkpoints { get; set; }

        public TrackingHistoryModel(List<TrackingHistory> checkpoints)
        {
            Checkpoints = checkpoints;
            
        }

    }

    /*public class TrackingHistory
    {
        public DateTime? ObjectCreated { get; set; }
        public ShippoEnums.TrackingStatus Status { get; set; }
        public string StatusDetails { get; set; }
        public DateTime? StatusDate { get; set; }
        public ShortAddress Location { get; set; }
    }*/

}
