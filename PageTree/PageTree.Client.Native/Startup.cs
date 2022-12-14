using PageTree.Client.Shared.Services.CQRS;

namespace PageTree.Client.Native;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddSingleton<IQueryExecutor, MediatorQueryExecutor>();
    }
}
