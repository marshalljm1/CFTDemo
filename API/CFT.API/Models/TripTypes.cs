using System;
using System.Collections.Generic;

namespace CFT.API.Models
{
    public partial class TripTypes
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool OvernightStay { get; set; }
        public bool Deposit { get; set; }
        public int TravelType { get; set; }
        public decimal Price { get; set; }
    }
}
