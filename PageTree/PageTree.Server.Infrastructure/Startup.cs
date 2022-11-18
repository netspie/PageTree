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

        private static void InitRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRepository<Page>>(default(IRepository<Page>));
            serviceCollection.AddSingleton<IRepository<Signature>>(default(IRepository<Signature>));
            serviceCollection.AddSingleton<IRepository<PracticeCategory>>(default(IRepository<PracticeCategory>));
            serviceCollection.AddSingleton<IRepository<PracticeTactic>>(default(IRepository<PracticeTactic>));
        }
    }
}
