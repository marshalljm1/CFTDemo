namespace CFT.Promotions.Core.Models
{
    public partial class TripTypes : BaseItem
    {
        public new int Id { get; set; }
        public string Code { get; set; }
        public new string Description { get; set; }
        public bool OvernightStay { get; set; }
        public bool Deposit { get; set; }
        public int TravelType { get; set; }
        public decimal Price { get; set; }
    }
}
