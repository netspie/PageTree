using Common.Infrastructure.MauiMsalAuth;
using Microsoft.Extensions.Logging;
using PageTree.Client.Native.Data;

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

        var baseAddress = "https://japanesearcana.com/";

#if DEBUG
        baseAddress = "http://192.168.178.44:5092/";
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddMsalAuthentication(builder.Configuration);
        builder.Services.AddTransient<MainPage>();

        builder.Services.AddAuthorizedHttpService<IWeatherForecastService, WeatherForecastService>(baseAddress);

        return builder.Build();
    }
}