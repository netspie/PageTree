using Common.Infrastructure.MauiMsalAuth;
using Microsoft.Extensions.Logging;
using PageTree.Client.Native.Auth;
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
        //SecureStorage.Default.RemoveAll();
        var baseAddress = "https://japanesearcana.com/";
#if DEBUG
        baseAddress = $"http://localhost:5092";

        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddHttpClient(AuthUserTypes.Authorized, baseAddress).AddHttpMessageHandler<AuthorizationMessageHandler>();
        builder.Services.AddHttpClient(AuthUserTypes.Anonymous, baseAddress);

        builder.Services.AddMediator();
        builder.Services.AddCQRS(baseAddress);

        builder.Services.AddSingleton<IAuthUser, NativeAuthUser>();
        builder.Services.AddMsalAuthentication(builder.Configuration);
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}
