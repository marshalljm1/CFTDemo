using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CFT.Promotions.Core;
using Plugin.Toasts;

namespace CFT.Promotions.Android
{
    [Activity(Label = "CFT.Promotions.Android", Icon = "@drawable/xamarin_logo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ToastNotification.Init(this);
            LoadApplication(new App());
        }
    }
}

