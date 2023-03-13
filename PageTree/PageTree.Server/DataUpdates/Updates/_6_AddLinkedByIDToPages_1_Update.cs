using Common.Basic.Blocks;
using Common.Basic.Repository;
using PageTree.Domain;

namespace PageTree.Server.DataUpdates
{
    public class _6_AddLinkedByIDToPages_1_Update : BaseDataUpdate
    {
        private readonly IRepository<Page> _pageRepository;

        public _6_AddLinkedByIDToPages_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<Page> pageRepository) : base(dataUpdateRepository)
        {
            _pageRepository = pageRepository;
        }

        protected override async Task<Result> PerformDataUpdate()
        {
            var res = Result.Success();

            var pages = await _pageRepository.GetAll(res);
            var pagesDict = pages.ToDictionary(p => p.ID);
            foreach (var page in pages)
            {
                var links = page.Links.ToList();
                foreach (var linkID in links)
                {
                    if (pagesDict.ContainsKey(linkID))
                        continue;
                    
                    page.RemoveProperty(linkID);
                    await _pageRepository.Save(page, res);
                }
            }

            return res;
        }
    }
}
