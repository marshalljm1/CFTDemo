using CFT.Promotions.Core.Models;
using CFT.Promotions.Core.Services;
using CFT.Promotions.Core.ViewModels;
using CFT.Promotions.Core.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CFT.Promotions.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpView : ContentPage
	{
	    private double _width;
	    private double _height;

	    public SignUpView (SignUpViewModel model)
		{
			InitializeComponent ();
		    BindingContext = model;
		    model.RefreshCommand.Execute(new object());
            
		}

	    protected override void OnSizeAllocated(double width, double height)
	    {
            base.OnSizeAllocated(width, height);

	        if (width != _width || height != _height)
	        {
	            _width = width;
	            _height = height;

	            if (width > height) //landscape mode
	            {
	                MainStack.Orientation = StackOrientation.Horizontal;
	                NameStack.Orientation = StackOrientation.Vertical;
	                BackgroundImage = "background_landscape.jpg";
	                scrollLabel.TextColor = Color.White;

	            }
	            else //portrait
	            {
	                MainStack.Orientation = StackOrientation.Vertical;
	                NameStack.Orientation = StackOrientation.Horizontal;
	                BackgroundImage = "background_portrait.jpg";
	                scrollLabel.TextColor = Color.Black;
	            }
	        }
	    }
	}
}