using System.Windows.Input;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;
using Xamarin.Forms;

namespace CFT.Promotions.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private Command _loginCommandCommand;
        public ICommand LoginCommand => _loginCommandCommand ?? (_loginCommandCommand = new Command(OnLogin));

        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public LoginViewModel(UserModel user, ICommonServices commonServices) : base(commonServices)
        {
            _user = user;
        }

        private void OnLogin()
        {
            //implement API call to login, will dummy this for now

            if (string.IsNullOrWhiteSpace(_user.UserName) ||
                string.IsNullOrWhiteSpace(_user.Password))
                return;

            Navigation.NavigateToAsync<MainViewModel>();
        }
    }
}
