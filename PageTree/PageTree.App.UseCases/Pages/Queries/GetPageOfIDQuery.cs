using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.Domain;
using PageTree.Domain.Practice;
using Practicer.Domain.Practice;

namespace PageTree.App.Pages.Queries;

public class GetPageOfIDQueryHandler : IQueryHandler<GetPageOfIDQuery, Result<GetPageOfIDQueryOut>>
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

    public async ValueTask<Result<GetPageOfIDQueryOut>> Handle(GetPageOfIDQuery query, CancellationToken cancellationToken)
    {
        var res = Result<GetPageOfIDQueryOut>.Success();

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
        return res.With(new GetPageOfIDQueryOut(
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

public sealed record CreatePageCommand() : ICommand<Result>;
public sealed record ReplacePageCommand(string PageID) : ICommand<Result>;
public sealed record DeletePageCommand(string PageID) : ICommand<Result>;
public sealed record ChangeNameOfPageCommand(string PageID, string NewName) : ICommand<Result>;
public sealed record ChangeSignatureOfPageCommand(string PageID, string NewSignatureName) : ICommand<Result>;

public sealed record GetPagesQuery() : IQuery<Result<GetPagesQueryOut>>;
public sealed record GetPagesQueryOut();

public sealed record GetPageOfIDQuery(string ID) : IQuery<Result<GetPageOfIDQueryOut>>;
public sealed record GetPageOfIDQueryOut(PageVM PageVM);

public class PageVM
{
    public IdentityVM[] Path { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; init; } = Array.Empty<PropertyVM>();
    public IdentityVM[] PracticeTactics { get; init; } = Array.Empty<IdentityVM>();
}

public class PropertyVM
{
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; init; } = Array.Empty<PropertyVM>();
}

public class IdentityVM
{
    public string ID { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}