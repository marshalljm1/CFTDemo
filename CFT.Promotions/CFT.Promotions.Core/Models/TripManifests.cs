using System;

namespace CFT.Promotions.Core.Models
{
    public partial class TripManifests : BaseItem
    {
        public new int Id { get; set; }
        public int TripId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool Paid { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
