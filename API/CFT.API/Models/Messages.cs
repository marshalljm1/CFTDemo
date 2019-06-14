using System;
using System.Collections.Generic;

namespace CFT.API.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
