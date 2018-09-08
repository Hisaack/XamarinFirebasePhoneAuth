using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using static Firebase.Auth.PhoneAuthProvider;

namespace XamarinFirebasePhoneAuth
{
    public class PhoneAuthCallbacks : OnVerificationStateChangedCallbacks, IOnCompleteListener
    {
        private string _verificationId;
        private ForceResendingToken _token;
        private IUpdatable updatable;

        public PhoneAuthCallbacks(IUpdatable updatable)
        {
            this.updatable = updatable;
        }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            SignInWithPhoneAuthCredential(credential);
        }

        private void SignInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {
            MainActivity._auth.SignInWithCredential(credential)
                .AddOnCompleteListener(this);

        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            throw new NotImplementedException();
        }

        public override void OnCodeSent(string verificationId, ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);
            _verificationId = verificationId;
            _token = forceResendingToken;
        }

        public void OnComplete(Task task)
        {
          updatable.IsSuccessful(true);
        }
    }
}