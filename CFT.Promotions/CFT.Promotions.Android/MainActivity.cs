using Android.App;
using Android.Content.PM;
using Android.OS;
using CFT.Promotions.Core;

namespace CFT.Promotions.Droid
{
    [Activity(Label = "CFT.Promotions.Android", Icon = "@drawable/xamarin_logo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

