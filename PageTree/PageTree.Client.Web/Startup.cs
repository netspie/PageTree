using Corelibs.BlazorShared;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Web;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        services.AddSingleton<IQueryExecutor, PageTreeQueryExecutor>();
        services.AddSingleton<ICommandExecutor, PageTreeCommandExecutor>();
    }
}
