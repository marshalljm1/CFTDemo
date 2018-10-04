using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CFT.Promotions.Core;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
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
            PayPal.Forms.CrossPayPalManager.Init(GetPaypalConfig(), this);
            ToastNotification.Init(this);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }

        private PayPalConfiguration GetPaypalConfig()
        {
#if DEBUG
            var key = "AVh7gj5w63Uq62VRtRjxHPOYAtMfsS6qqjRgp-Na9XGhz71EDZMPTKP7pb8eELy-KqgviK4R-bwwfRV7";
            var env = PayPalEnvironment.Sandbox;
#else
            var key = "Aazc4nuodVb5Ebr73hE55udv4t64Huq_rMjw_sdafth1nGCBrhpinBUXf_n9i1dfn_mYHGZsbpQqSjOd";
            var env = PayPalEnvironment.Production;
#endif

            return new PayPalConfiguration(env, key)
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "Clark Family Tours Inc.",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://www.clarkfamilytours.com",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://www.clarkfamilytours.com",
                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                ShippingAddressOption = ShippingAddressOption.Both,
                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "en",
                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "1",

                StoreUserData = false
            };
        }
    }
}

