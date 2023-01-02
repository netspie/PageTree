using Microsoft.Extensions.DependencyInjection;

namespace PageTree.Server.ApiContracts
{
    public static class ApiContractsExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Pages_ApiToApp_MapProfile));
        }
    }
}
