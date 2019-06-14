namespace CFT.Promotions.Core.Models
{
    public class CCTransData
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public Trips Trip { get; set; }
    }
}
