using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using Firebase;
using Firebase.Auth;
using Java.Util.Concurrent;

namespace XamarinFirebasePhoneAuth
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnCountryPickerListener, IUpdatable
    {
        private TextInputEditText _codeInputEditText;
        private TextInputEditText _phoneNoInputEditText;
        private Button _signInButton;
        public static FirebaseAuth _auth;
        public static FirebaseApp _app;
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
            InitFirebaseAuth();
            var callbacks=new PhoneAuthCallbacks(this);
            PhoneAuthProvider.GetInstance(_auth)
                .VerifyPhoneNumber(
                    phoneNo,
                    2,
                    TimeUnit.Minutes,
                    this,
                    callbacks);
        }

        private void InitFirebaseAuth()
        {
            if (_app == null)
                _app = FirebaseApp.InitializeApp(this);
            _auth=new FirebaseAuth(_app);
        }

        public void UpdateUi()
        {
            _codeInputEditText.Visibility = ViewStates.Invisible;
            _phoneNoInputEditText.Visibility = ViewStates.Invisible;
            _signInButton.Text = "Continue";
        }

        public void OnSelectCountry(Country country)
        {
            _codeInputEditText.Text = country.DialCode;
        }

        public void IsSuccessful(bool isSuccessful)
        {
            if(isSuccessful)
                UpdateUi();
        }
    }
}