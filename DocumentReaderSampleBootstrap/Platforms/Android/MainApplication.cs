using Android.App;
using Android.Runtime;
using Com.Regula.Documentreader.Api;
using Com.Regula.Documentreader.Api.Enums;
using DocumentReaderSample.Platforms.Android;

namespace DocumentReaderSample.Bootstrap.bpi
{
    [Application]
    public class MainApplication : MauiApplication
    {

        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            DependencyService.Register<IDocReaderInit, DocReaderInit>();
            DependencyService.Register<IDocReaderScanner, DocReaderScanner>();
            DependencyService.Register<IPhotoPickerService, PhotoPickerService>();

        }

        protected override MauiApp CreateMauiApp()
        {
            return MauiProgram.CreateMauiApp();
        }


 

        public void Regula()
        {
            DocumentReader.Instance().Customization().Edit().SetCameraFrameBorderWidth((Java.Lang.Integer)5).Apply();
            DocumentReader.Instance().Customization().Edit().SetTintColor("#009900").Apply();
            DocumentReader.Instance().Functionality().Edit().SetShowSkipNextPageButton(false).Apply();
            DocumentReader.Instance().Functionality().Edit().SetShowCameraSwitchButton(true).Apply();
            DocumentReader.Instance().Functionality().Edit().SetShowTorchButton(true).Apply();

            DocumentReader.Instance().ProcessParams().DateFormat = "dd-mm-yyyy";
            DocumentReader.Instance().ProcessParams().Scenario = Com.Regula.Documentreader.Api.Enums.Scenario.ScenarioOcr;
        }
    }


}