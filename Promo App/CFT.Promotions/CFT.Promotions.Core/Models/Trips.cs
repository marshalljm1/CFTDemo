using System;

namespace CFT.Promotions.Core.Models
{
    public partial class Trips
    {
        public new int Id { get; set; }
        public int AssignedBus { get; set; }
        public int TripType { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureCity { get; set; }
        public new string Description { get; set; }

    }
}
