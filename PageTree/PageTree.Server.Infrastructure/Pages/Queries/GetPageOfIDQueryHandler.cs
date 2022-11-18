using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Pages.Queries;
using PageTree.Domain;
using PageTree.Domain.Practice;
using Practicer.Domain.Practice;
using QueryResult = System.Threading.Tasks.ValueTask<Common.Basic.Blocks.Result<PageTree.App.Pages.Queries.GetPageOfIDQueryDTO>>;

namespace PageTree.Server.Infrastructure.Pages.Queries;

public class GetPageOfIDQueryHandler : IQueryHandler<GetPageOfIDQuery, Result<GetPageOfIDQueryDTO>>
{
    private IRepository<Page> _pageRepository;
    private IRepository<Signature> _signatureRepository;
    private IRepository<PracticeCategory> _practiceCategoryRepository;
    private IRepository<PracticeTactic> _practiceTacticRepository;

    public GetPageOfIDQueryHandler(
        IRepository<Page> pageRepository, 
        IRepository<Signature> signatureRepository, 
        IRepository<PracticeCategory> practiceCategoryRepository, 
        IRepository<PracticeTactic> practiceTacticRepository)
    {
        _pageRepository = pageRepository;
        _signatureRepository = signatureRepository;
        _practiceCategoryRepository = practiceCategoryRepository;
        _practiceTacticRepository = practiceTacticRepository;
    }

    public async QueryResult Handle(GetPageOfIDQuery query, CancellationToken cancellationToken)
    {
        var res = Result<GetPageOfIDQueryDTO>.Success();

        var practiceCategoryRoot = await _practiceCategoryRepository.Get("PracticeCategoryRootID", res);
        var practiceTacticRoot = await _practiceTacticRepository.Get("PracticeTacticRootID", res);

        var practiceCategories = await _practiceCategoryRepository.Get(practiceCategoryRoot.Items, res);
        var practiceTactics = await _practiceTacticRepository.Get(practiceTacticRoot.Items, res);

        var page = await _pageRepository.Get(query.ID, res);
        var signature = await _signatureRepository.Get(page.SignatureID, res);

        async Task GetParentPage(Page page, List<Page> pages)
        {
            if (string.IsNullOrEmpty(page.ParentID))
                return;

            var parentPage = await _pageRepository.Get(page.ParentID, res);
            pages.Add(parentPage);
            await GetParentPage(parentPage, pages);
        }

        var pages = new List<Page>();
        await GetParentPage(page, pages);
        pages.Reverse();
        return res.With(new GetPageOfIDQueryDTO(
            new PageVM()
            {
                Path = pages
                    .Select(p => new IdentityVM()
                    {
                        ID = p.ID,
                        Name = p.Name
                    }).ToArray(),

                Identity = new IdentityVM()
                {
                    ID = page.ID,
                    Name = page.Name,
                },

                SignatureIdentity = new IdentityVM()
                {
                    ID = signature?.ID ?? string.Empty,
                    Name = signature?.Name ?? string.Empty
                },

                Properties = await GetProperties(page),

                PracticeTactics = practiceTactics.Select(p => new IdentityVM()
                {
                    ID = p.ID,
                    Name = p.Name
                }).ToArray()

            }
        ));
    }

    public async Task<PropertyVM[]> GetProperties(Page currentPage)
    {
        var res = Result.Success();

        var result = new List<PropertyVM>();
        foreach (var childID in currentPage.ChildrenIDs)
        {
            var childPage = await _pageRepository.Get(childID, res);
            var childSignature = !string.IsNullOrEmpty(childPage.SignatureID) ?
                await _signatureRepository.Get(childPage.SignatureID, res) :
                new Signature();

            result.Add(new PropertyVM()
            {
                Identity = new IdentityVM()
                {
                    ID = childPage.ID,
                    Name = childPage.Name
                },

                SignatureIdentity = new IdentityVM()
                {
                    ID = childSignature.ID,
                    Name = childSignature.Name
                },

                //Properties = await GetProperties(childPage)

            });
        }

        return result.ToArray();
    }
}
