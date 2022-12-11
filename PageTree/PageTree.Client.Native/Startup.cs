using Mediator;
using PageTree.Client.Shared.CQRS;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Native;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()));

        services.AddTransient<IDataService>(sp =>
            new HttpDataService(sp.GetRequiredService<HttpClient>()));
    }
}
