using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;

namespace XamarinFirebasePhoneAuth
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnCountryPickerListener
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

            _codeInputEditText.Focusable = false;
            _codeInputEditText.Click += (s, e) =>
            {
                new CountryPicker.Builder()
                    .With(this)
                    .Listener(this)
                    .Build()
                    .ShowDialog(SupportFragmentManager);
            };
            _signInButton.Click += _signInButton_Click;
        }

        private void _signInButton_Click(object sender, System.EventArgs e)
        {
            if(string.IsNullOrEmpty(_phoneNoInputEditText.Text)) 
                Toast.MakeText(this,"Phone Number Cannot be empty", ToastLength.Short).Show();
            else if(string.IsNullOrEmpty(_codeInputEditText.Text))
                Toast.MakeText(this, "Country code cannot be empty", ToastLength.Short).Show();
            else
            {
                SignInUser(string.Concat(_codeInputEditText.Text, _phoneNoInputEditText.Text));
            }

        }

        private void SignInUser(string phoneNo)
        {
                Toast.MakeText(this, "Lets do  it", ToastLength.Short).Show();
        }

        public void OnSelectCountry(Country country)
        {
            _codeInputEditText.Text = country.DialCode;
        }
    }
}