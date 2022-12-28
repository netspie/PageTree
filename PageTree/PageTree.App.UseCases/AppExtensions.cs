using Microsoft.Extensions.DependencyInjection;

namespace PageTree.App.UseCases
{
    public static class AppExtensions
    {
        public static void AddMediatorExt(this IServiceCollection services)
        {
            services.AddMediator(opts => opts.ServiceLifetime = ServiceLifetime.Scoped);
        }
    }
}
