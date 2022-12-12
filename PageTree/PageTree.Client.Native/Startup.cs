using Mediator;
using PageTree.Client.Shared.CQRS;

namespace PageTree.Client.Native;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services, string baseAdress)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()));

        //services.AddTransient<IDataService>(sp =>
        //    new HttpDataService(sp.GetRequiredService<IHttpClientFactory>()));

        //services
        //    .AddHttpClient<IDataService, DataServiceTryCatchDecorator<AccessTokenNotAvailableException>>(
        //        (client, sp) =>
        //        {
        //            client.BaseAddress = new Uri(baseAdress);
        //            return new DataServiceTryCatchDecorator<AccessTokenNotAvailableException>(
        //                new HttpDataService(sp.GetRequiredService<IHttpClientFactory>()),
        //                    onCatch: ex => ex.Redirect());
        //        })
        //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
    }
}
