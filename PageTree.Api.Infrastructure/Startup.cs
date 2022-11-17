using BuildingBlocks.Repository;
using Common.Basic.Repository;
using Microsoft.Extensions.DependencyInjection;
using PageTree.Domain;
using PageTree.Domain.Practice;
using Practicer.Domain.Practice;

namespace PageTree.Api.Infrastructure
{
    public class Startup
    {
        public static void Run(IServiceCollection serviceCollection)
        {
            InitRepositories(serviceCollection);
        }

        private static void InitRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRepository<Page>>(new WWWRootHttpClientRepository<Page>());
            serviceCollection.AddSingleton<IRepository<Signature>>(new WWWRootHttpClientRepository<Signature>());
            serviceCollection.AddSingleton<IRepository<PracticeCategory>>(new WWWRootHttpClientRepository<PracticeCategory>());
            serviceCollection.AddSingleton<IRepository<PracticeTactic>>(new WWWRootHttpClientRepository<PracticeTactic>());
        }
    }
}
