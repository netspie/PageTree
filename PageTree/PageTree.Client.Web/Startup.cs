using Corelibs.BlazorShared;
using Mediator;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace PageTree.Client.Web;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddSingleton<IQueryExecutor>(sp =>
            new QueryExecutorTryCatchDecorator<AccessTokenNotAvailableException>(
                new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()), onCatch: ex => ex.Redirect()));
    }
}
