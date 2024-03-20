using Android.Runtime;
using Android.Content;
using System.Diagnostics;
using Android.App;
using Android.OS;


namespace DocumentReaderSample.Bootstrap.bpi
{
    [Activity(Name = "eu.bpiservices.bpi.DocumentReaderSample.Bootstrap.MainActivity", Theme = "@style/Maui.MainTheme.NoActionBar")]
    public class MainActivity : MauiAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Instance = this;

            //Set QR code if link was used
            var data = Intent?.Data?.Query;
            string qrcode = null;
            if (!string.IsNullOrEmpty(data))
            {
                qrcode = Intent?.Data?.GetQueryParameter("qrcode");                
            }

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    // Set the filename as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(data.DataString);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.FinishActivity(0);
        }









    }
}