using System;
using Xamarin.Forms;

namespace CFT.Promotions.Core.Models
{
    public class MasterPageItem
    {
        public string Title { get; set; }
        public ImageSource IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}
