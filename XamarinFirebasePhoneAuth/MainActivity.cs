using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Widget;

namespace XamarinFirebasePhoneAuth
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextInputEditText _codeInputEditText;
        private TextInputEditText _phoneNoInputEditText;
        private Button _signInButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _codeInputEditText = FindViewById<TextInputEditText>(Resource.Id.countryCodeTxtInputEdtTxt);
            _phoneNoInputEditText =FindViewById<TextInputEditText>(Resource.Id.phoneNumberTxtInputEdtTxt);
            _signInButton = FindViewById<Button>(Resource.Id.authorizeVerificationBtn);
        }
    }
}