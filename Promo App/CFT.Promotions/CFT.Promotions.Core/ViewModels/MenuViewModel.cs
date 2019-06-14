using System.Collections.Generic;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;

namespace CFT.Promotions.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public List<MasterPageItemViewModel> MenuItems { get; }

        public MenuViewModel(ICommonServices commonServices) : base(commonServices)
        {
            MenuItems = new List<MasterPageItemViewModel>
            {
                new MasterPageItemViewModel(
                    new MasterPageItem
                    {
                        Title = "New Client",
                        IconSource = "xamarin_logo.png",
                        //TargetType = typeof(ClientRegistrationViewModel)
                    },
                    commonServices
                )
            };
        }
    }
}
