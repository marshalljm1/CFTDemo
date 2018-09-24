using System;
using System.Windows.Input;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;
using Xamarin.Forms;

namespace CFT.Promotions.Core.ViewModels
{
    public class MasterPageItemViewModel : ViewModelBase
    {
        private readonly MasterPageItem _menuItem;

        private Command _navCommand;
        public ICommand NavigateCommand => _navCommand ?? (_navCommand = new Command(NavigateToPage));

        public MasterPageItemViewModel(MasterPageItem item, ICommonServices commonServices) : base(commonServices)
        {
            _menuItem = item;
        }

        private void NavigateToPage()
        {
            Navigation.NavigateToAsync(TargetType);
        }

        public new string Title => _menuItem.Title;
        public ImageSource IconSource => _menuItem.IconSource;
        public Type TargetType => _menuItem.TargetType;

    }
}
