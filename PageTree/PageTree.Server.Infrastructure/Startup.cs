using BuildingBlocks.Repository;
using Common.Basic.Repository;
using Microsoft.Extensions.DependencyInjection;
using PageTree.Domain;
using PageTree.Domain.Practice;
using Practicer.Domain.Practice;

namespace PageTree.Server.Infrastructure
{
    public class Startup
    {
        public static void Run(IServiceCollection serviceCollection)
        {
            InitRepositories(serviceCollection);
        }

        private static void InitRepositories(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Page>>(sp =>
               new WWWRootHttpClientRepository<Page>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<Signature>>(sp =>
               new WWWRootHttpClientRepository<Signature>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<PracticeCategory>>(sp =>
               new WWWRootHttpClientRepository<PracticeCategory>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<PracticeTactic>>(sp =>
               new WWWRootHttpClientRepository<PracticeTactic>(sp.GetRequiredService<HttpClient>(), "", ""));
        }
    }
}
