using System;
using System.Collections.Generic;

namespace CFT.Promotions.Core.Models
{
    public partial class Messages : BaseItem
    {
        public new int Id { get; set; }
        public string Message { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
