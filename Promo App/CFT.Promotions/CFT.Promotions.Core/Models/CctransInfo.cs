namespace CFT.Promotions.Core.Models
{
    public class CctransInfo : BaseItem
    {
        public new int Id { get; set; }
        public int ManifestId { get; set; }
        public string TransactionId { get; set; }
        public string AuthCode { get; set; }
        public new string Description { get; set; }
        public string ResponseCode { get; set; }
        public string MessageCode { get; set; }

        public TripManifests Manifest { get; set; }
    }
}
