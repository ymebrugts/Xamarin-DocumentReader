using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace DocumentReaderSample.Bootstrap.bpi
{
    [Activity(Name = "eu.bpiservices.bpi.DocumentReaderSample.Bootstrap.SplashScreen", Theme = "@style/MyTheme.Splash", NoHistory = true, MainLauncher = true)]
    public class SplashScreen : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashLayout);
            System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        private async void LoadActivity()
        {
            await Task.Delay(1000);
            RunOnUiThread(() => { 
            string action = Intent.Action;
            string strLink = Intent.DataString;
            Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
            if (Android.Content.Intent.ActionView == action && !string.IsNullOrWhiteSpace(strLink))
            {
                intent.SetAction(Intent.ActionView);
                intent.SetData(Intent.Data);
            }
                StartActivity(intent);
            });
        }

        public override void OnBackPressed() { }

    }
}