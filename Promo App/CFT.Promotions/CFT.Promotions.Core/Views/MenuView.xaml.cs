using CFT.Promotions.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CFT.Promotions.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        public MenuView(MenuViewModel menuViewModel)
        {
            InitializeComponent();
            BindingContext = menuViewModel;
            Title = "Master";
            BackgroundColor = Color.Gray.WithLuminosity(0.9);
            Icon = "xamarin_logo.png";
        }
    }
}