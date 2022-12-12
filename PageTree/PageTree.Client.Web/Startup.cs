﻿using Mediator;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PageTree.Client.Shared.CQRS;
using PageTree.Client.Shared.Services;
using PageTree.Client.Web.Auth;

namespace PageTree.Client.Web;

public static class Startup
{
    public static void AddCQRSRepositories(this IServiceCollection services)
    {
       
    }

    public static void AddCQRS(this IServiceCollection services, string baseAdress)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new QueryExecutorTryCatchDecorator<AccessTokenNotAvailableException>(
                new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()), onCatch: ex => ex.Redirect()));

        //services
        //    .AddHttpClient<IDataService, HttpDataService<AccessTokenNotAvailableException>>()
        //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        services.AddSingleton<ISignInRedirector, WebSignInRedirector>();

        services
            .AddHttpClient<IDataService, HttpDataService<AccessTokenNotAvailableException>>(
            (client, sp) =>
            {
                client.BaseAddress = new Uri(baseAdress);
                return new HttpDataService<AccessTokenNotAvailableException>(sp.GetRequiredService<IHttpClientFactory>(), sp.GetRequiredService<ISignInRedirector>());
            })
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        //services
        //    .AddHttpClient<IDataService, DataServiceTryCatchDecorator<AccessTokenNotAvailableException>>(
        //        (client, sp) =>
        //        {
        //            client.BaseAddress = new Uri(baseAdress);
        //            return new DataServiceTryCatchDecorator<AccessTokenNotAvailableException>(
        //                new HttpDataService<AccessTokenNotAvailableException>(sp.GetRequiredService<IHttpClientFactory>()), 
        //                onCatch: ex => ex.Redirect());
        //        })
        //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
    }
}
