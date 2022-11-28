using PageTree.Client.Native.Data;
using PageTree.Client.Native.MsalClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace PageTree.Client.Native;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        var executingAssembly = Assembly.GetExecutingAssembly();
        using var stream = executingAssembly.GetManifestResourceStream("PageTree.Client.Native.appsettings.json");
        var configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<IPCAWrapper, PCAWrapper>();
        builder.Configuration.AddConfiguration(configuration);

        builder.Services.AddSingleton<WeatherForecastService>();

        return builder.Build();
    }
}