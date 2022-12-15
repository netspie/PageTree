using Corelibs.BlazorShared;

namespace PageTree.Client.Native;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddSingleton<IQueryExecutor, MediatorQueryExecutor>();
    }
}
