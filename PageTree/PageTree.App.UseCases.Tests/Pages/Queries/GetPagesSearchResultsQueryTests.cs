using Corelibs.AspNetApi.Lucene;
using PageTree.App.Pages.Queries;
using PageTree.Domain;
using Corelibs.Basic.Searching;

namespace PageTree.Server.UseCases.Tests.Pages.Queries
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Handle()
        {
            //var searchEngine = new LuceneInRamSearchEngine<Page>();
            //var handler = new GetPagesSearchResultsQueryHandler(searchEngine, null, null);

            //searchEngine.Add("ID-1", "Page-1");
            //searchEngine.Add("ID-2", "Page-2");
            //searchEngine.Add("ID-3", "Page-3");

            //// handler.Handle(new GetPagesSearchResultsQuery());

            //Assert.Pass();
        }
    }
}
