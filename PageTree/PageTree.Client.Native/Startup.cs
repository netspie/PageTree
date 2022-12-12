using Common.Infrastructure.MauiMsalAuth;
using Mediator;
using PageTree.Client.Native.Auth;
using PageTree.Client.Shared.CQRS;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Native;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services, string baseAdress)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()));

        services.AddTransient<ISignInRedirector, NativeSignInRedirector>();

        services
            .AddHttpClient<IDataService, HttpDataService<NoAccessTokenAvailableException>>(
            (client, sp) =>
            {
                client.BaseAddress = new Uri(baseAdress);
                return new HttpDataService<NoAccessTokenAvailableException>(sp.GetRequiredService<IHttpClientFactory>(), sp.GetRequiredService<ISignInRedirector>());
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>();
    }
}
