using Mediator;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PageTree.Client.Shared.CQRS;

namespace PageTree.Client.Web;

public static class Startup
{
    public static void AddCQRSRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new QueryExecutorTryCatchDecorator<AccessTokenNotAvailableException>(
                new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()), onCatch: ex => ex.Redirect()));
    }

    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new QueryExecutorTryCatchDecorator<AccessTokenNotAvailableException>(
                new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()), onCatch: ex => ex.Redirect()));
    }
}
