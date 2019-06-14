using CFT.Promotions.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CFT.Promotions.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext = loginViewModel;
        }
    }
}