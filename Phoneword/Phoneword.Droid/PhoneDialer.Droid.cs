using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Phoneword.Droid;
using Xamarin.Forms;
using Android.Telephony;

[assembly: Dependency(typeof(PhoneDialer))]
namespace Phoneword.Droid
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            var context = Forms.Context;
            if (context != null)
            {
                var intent = new Intent(Intent.ActionCall);
                intent.SetData(Android.Net.Uri.Parse("tel:" + number));
                if(IsIntentAvailable(context,intent))
                {
                    context.StartActivity(intent);
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Checks if an intent can be handled.
        /// </summary>
		public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            Android.Telephony.TelephonyManager mgr = Android.Telephony.TelephonyManager.FromContext(context);
            return mgr.PhoneType != PhoneType.None;
        }
    }

    
}