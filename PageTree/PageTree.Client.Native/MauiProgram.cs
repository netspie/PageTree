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

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddMsalAuthentication(builder.Configuration);

        builder.Services.AddTransient<MainPage>();

        //httpClientBuilder.
        //builder.Services.AddSingleton<WeatherForecastService>();
        //builder.Services.AddHttpClient<WeatherForecastService>().AddHttpMessageHandler<AuthorizationMessageHandler>();

        return builder.Build();
    }
}
