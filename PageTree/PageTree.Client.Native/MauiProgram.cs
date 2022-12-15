using Common.Infrastructure.MauiMsalAuth;
using Corelibs.BlazorShared;
using Microsoft.Extensions.Logging;
using PageTree.Client.Native.Auth;

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
        //baseAddress = $"http://192.168.178.44:5259";

        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddCQRS();
        builder.Services.AddMsalAuthentication(builder.Configuration);
        builder.Services.AddAuthorizationAndSignInRedirection<
            NativeAuthUser, NativeSignInRedirector, NoAccessTokenAvailableException, AuthorizationMessageHandler>(
            baseAddress);

        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}
