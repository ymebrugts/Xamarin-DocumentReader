using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;


namespace DocumentReaderSample.Bootstrap;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
        .UseMauiCommunityToolkit()
        .UseMauiCommunityToolkitMediaElement()
        .UseMauiCompatibility()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("calibri.ttf", "calibri");
            fonts.AddFont("Segoe_UI.ttf", "segoe_ui");
            fonts.AddFont("materialdesignicons_webfont.ttf", "materialdesigniconswebfont");
            fonts.AddFont("Segoe_UI_Bold_Italic.ttf", "segoe_ui_bold_italic");
        })
        
        .UseMauiApp<App>((builder) => new App())
#if IOS
#endif
#if ANDROID
        .Services
        .AddSingleton(x => Platform.AppContext)
        .AddSingleton(x => Platform.CurrentActivity)

#endif
        ;

        var app = builder.Build();

        ServiceHelper.Initialize(app.Services);

        return app;
    }
}