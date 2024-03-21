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

        return app;
    }
}