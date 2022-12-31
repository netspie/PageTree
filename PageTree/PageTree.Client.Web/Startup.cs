using Corelibs.BlazorShared;
using PageTree.Client.Shared.Services;
using PageTree.Server.ApiContracts;

namespace PageTree.Client.Web;

public static class Startup
{
    public static void AddCQRS(this IServiceCollection services)
    {
        ApiContractsExtensions.AddAutoMapper(services);

        services.AddSingleton<IQueryExecutor, PageTreeQueryExecutor>();
        services.AddSingleton<ICommandExecutor, PageTreeCommandExecutor>();
    }
}
