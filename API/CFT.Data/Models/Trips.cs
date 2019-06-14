using System;
using System.Collections.Generic;

namespace CFT.Data.Models
{
    public partial class Trips
    {
        public int Id { get; set; }
        public int AssignedBus { get; set; }
        public int TripType { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureCity { get; set; }
        public string Description { get; set; }
    }
}
