namespace CFT.API.Models
{
    public partial class Trips : BaseItem
    {
        public string FormattedCityDescription => DestinationCity + " " + Description;
        public string FormattedDates => DepartureDate.ToString("d") + " - " + ReturnDate.ToString("d");
    }

    public partial class Buses : BaseItem { }
    public partial class Messages : BaseItem { }
    public partial class Travelers : BaseItem { }
    public partial class TravelTypes : BaseItem { }
    public partial class TripManifests : BaseItem { }
    public partial class TripTypes : BaseItem { }
    public partial class Users : BaseItem { }
}
