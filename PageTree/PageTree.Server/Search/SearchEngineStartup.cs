using Common.Basic.Repository;
using Corelibs.Basic.Searching;
using PageTree.Domain;

namespace PageTree.Server.Search
{
    public static class SearchEngineStartup
    {
        public static async Task IndexSearchEngine(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var pageRepository = scope.ServiceProvider.GetRequiredService<IRepository<Page>>();
                var pageSearchEngine = scope.ServiceProvider.GetRequiredService<ISearchEngine<Page>>();

                var pagesResult = await pageRepository.GetAll();
                var pages = pagesResult.Get();

                var searchIndexData = pages.Select(p => new SearchIndexData(p.ID, p.Name)).ToArray();
                var result = pageSearchEngine.Add(searchIndexData);
            }
        }
    }
}
