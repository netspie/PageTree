using Microsoft.Extensions.DependencyInjection;

namespace PageTree.Client.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddHttpClient(this IServiceCollection services, string name, string adress)
            => services.AddHttpClient(name, client => client.BaseAddress = new Uri(adress));

        public static T Get<T>(this IServiceProvider sp)
            => sp.GetRequiredService<T>();
    }
}
