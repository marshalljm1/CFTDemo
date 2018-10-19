using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFT.Promotions.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CFT.Promotions.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCardView : ContentPage
    {
        public CreditCardView(CreditCardViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}