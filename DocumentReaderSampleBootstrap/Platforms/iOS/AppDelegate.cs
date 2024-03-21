using System;
using System.Collections.Generic;
using DocumentReaderSample.Platforms.iOS;
using Foundation;
using UIKit;

namespace DocumentReaderSample.Bootstrap.bpi
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp()
        {
            _ = new DocReaderCore.iOS.DocumentReader();
            _ = new BTDevice.iOS.RGLBTManager();

            DependencyService.Register<IDocReaderInit, DocReaderInit>();
            DependencyService.Register<IDocReaderScanner, DocReaderScanner>();
            DependencyService.Register<IPhotoPickerService, PhotoPickerService>();

            return MauiProgram.CreateMauiApp();
        }




        private string _qrCode = null;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            return base.FinishedLaunching(app, options);
        }

        public override void WillEnterForeground(UIApplication application)
        {
            Window?.RootViewController?.DismissViewController(false, null);
        }
        public override void DidEnterBackground(UIApplication application)
        {
            var blankViewController = new UIViewController();
            blankViewController.View.BackgroundColor = UIColor.Black;
            Window?.RootViewController?.PresentViewController(blankViewController, false, null);
        }

        [Foundation.Export("application:OpenUrl:")]
        public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
        {
            var normalizedUrl = Uri.UnescapeDataString(url.ToString());

            var urlC = NSUrlComponents.FromUrl(url, false);

            var items = urlC.QueryItems;
            foreach (NSUrlQueryItem item in items)
            {
                if (item.Name == "qrcode")
                {
                    _qrCode = item.Value;
                }
            }

            MessagingCenter.Send<object, object>(this, "OpenIDScanWithUrl", _qrCode);

            return true;

        }


    }
}
