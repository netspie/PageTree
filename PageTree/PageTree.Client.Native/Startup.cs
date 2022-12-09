using Mediator;
using PageTree.Client.SharedPages.CQRS;

namespace PageTree.Client.Native;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddSingleton<IMQueryExecutor>(sp =>
            new MediatorQueryExecutor(sp.GetRequiredService<IMediator>()));
    }
}
