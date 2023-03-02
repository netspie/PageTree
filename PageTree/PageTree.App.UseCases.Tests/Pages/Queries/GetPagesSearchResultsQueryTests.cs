using Corelibs.AspNetApi.Lucene;
using PageTree.App.Pages.Queries;
using PageTree.Domain;

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
            var searchEngine = new LuceneInRamSearchEngine<Page>();
            var handler = new GetPagesSearchResultsQueryHandler(searchEngine, null, null);

            //handler.Handle(new GetPagesSearchResultsQuery());

            Assert.Pass();
        }
    }
}
