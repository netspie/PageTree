using Common.Basic.Common.Basic.Net;
using Common.Infrastructure.MauiMsalAuth;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PageTree.Client.Native.Auth;
using PageTree.Client.Native.Data;
using PageTree.Client.Shared;
using PageTree.Client.Shared.Services;

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
        baseAddress = $"{NetSocketExtensions.GetLocalIPAddress()}:9229";

        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddHttpClient("authorized", baseAddress).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
        builder.Services.AddHttpClient("anonymous", baseAddress);

        builder.Services.AddMediator();
        builder.Services.AddCQRS(baseAddress);

        builder.Services.AddSingleton<IAuthUser, NativeAuthUser>();
        builder.Services.AddMsalAuthentication(builder.Configuration);
        builder.Services.AddTransient<MainPage>();


        builder.Services.AddAuthorizedHttpService<IWeatherForecastService, WeatherForecastService>(baseAddress);

        return builder.Build();
    }
}
