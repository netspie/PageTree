using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;
using PageTree.App.Entities.Styles;
using PageTree.App.UseCases.Pages.Queries.Styles;
using Common.Basic.Collections;

namespace PageTree.App.Pages.Queries;

public class GetPageQueryHandler : IQueryHandler<GetPageQuery, Result<GetPageQueryOut>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<Style> _styleRepository;
    private readonly IRepository<Signature> _signatureRepository;
    private readonly IRepository<PracticeCategory> _practiceCategoryRepository;
    private readonly IRepository<PracticeTactic> _practiceTacticRepository;

    public GetPageQueryHandler(
        IRepository<Project> projectRepository,
        IRepository<Page> pageRepository,
        IRepository<Style> styleRepository,
        IRepository<Signature> signatureRepository,
        IRepository<PracticeCategory> practiceCategoryRepository,
        IRepository<PracticeTactic> practiceTacticRepository)
    {
        _projectRepository = projectRepository;
        _pageRepository = pageRepository;
        _styleRepository = styleRepository;
        _signatureRepository = signatureRepository;
        _practiceCategoryRepository = practiceCategoryRepository;
        _practiceTacticRepository = practiceTacticRepository;
    }

    public async ValueTask<Result<GetPageQueryOut>> Handle(GetPageQuery query, CancellationToken cancellationToken)
    {
        var res = Result<GetPageQueryOut>.Success();

        // Get public version !?
        var page = await _pageRepository.Get(query.ID, res);
        var signature = await _signatureRepository.Get(page.SignatureID, res);

        var project = await _projectRepository.Get(page.ProjectID, res);
        var signaturesRoot = await _signatureRepository.Get(project.SignatureRootID, res);
        var practiceCategoryRoot = await _practiceCategoryRepository.Get(project.PracticeCategoryRootID, res);
        var practiceTacticRoot = await _practiceTacticRepository.Get(project.PracticeTacticRootID, res);

        var practiceCategories = await _practiceCategoryRepository.Get(practiceCategoryRoot.Items, res);
        var practiceTactics = await _practiceTacticRepository.Get(practiceTacticRoot.Items, res);

        //var projectStyle = await _styleRepository.Get(project?.StyleID, res);
        //var signatureStyles = await _styleRepository.Get(signature?.StyleIDs, res);
        //var pageStyle = await _styleRepository.Get(page?.StyleID, res);
        //var finalStyle = projectStyle?.Override(signatureStyles.Append(pageStyle).ToArray());

        var mainStyle = HardcodedStyles.GetMainStyle(
            new IdentityVM() { ID = page.ID, Name = page.Name },
            new IdentityVM() { ID = signature?.ID, Name = signature?.Name });

        async Task GetParentPage(Page page, List<Page> pages)
        {
            if (string.IsNullOrEmpty(page.ParentID))
                return;

            var parentPage = await _pageRepository.Get(page.ParentID, res);
            pages.Add(parentPage);
            await GetParentPage(parentPage, pages);
        }

        var parentPages = new List<Page>();
        await GetParentPage(page, parentPages);
        parentPages.Reverse();

        var properties = await GetProperties(page, mainStyle?.TreeExpandInfo);
        var signatures = await _signatureRepository.Get(signaturesRoot.ChildrenIDs, res);

        return res.With(new GetPageQueryOut(
            new PageVM()
            {
                Path = parentPages
                    .Select(p => new IdentityVM()
                    {
                        ID = p.ID,
                        Name = p.Name
                    }).ToArray(),

                Identity = (page.ID, page.Name),

                SignatureIdentity = new IdentityVM()
                {
                    ID = signature?.ID ?? string.Empty,
                    Name = signature?.Name ?? string.Empty
                },

                Properties = properties,

                Signatures = signatures.Select(s => new IdentityVM(s.ID, s.Name)).ToArray(),

                PracticeTactics = practiceTactics.Select(p => new IdentityVM(p.ID, p.Name)).ToArray(),

                StyleOfPage = mainStyle
            }
        ));
    }

    public async Task<PropertyVM[]> GetProperties(Page currentPage, ExpandInfo expandInfo)
    {
        var res = Result.Success();

        var result = new List<PropertyVM>();

        for (int i = 0; i < currentPage.ChildrenIDs.Count; i++)
        {
            var childID = currentPage.ChildrenIDs[i];

            ExpandInfo childExpandInfo = null;
            if (expandInfo != null && !expandInfo.Children.IsNullOrEmpty() && expandInfo.Children.Count >= i + 1)
                childExpandInfo = expandInfo.Children[i];

            var childPage = await _pageRepository.Get(childID, res);
            var childSignature = !string.IsNullOrEmpty(childPage.SignatureID) ?
                await _signatureRepository.Get(childPage.SignatureID, res) :
                new Signature();

            var properties = Array.Empty<PropertyVM>();
            if ((expandInfo != null && expandInfo.AreChildrenExpanded) ||
                 childExpandInfo != null && childExpandInfo.IsExpanded)
                properties = await GetProperties(childPage, childExpandInfo);

            result.Add(new PropertyVM()
            {
                Identity = (childPage.ID, childPage.Name),
                SignatureIdentity = (childSignature.ID, childSignature.Name),
                Properties = properties
            });
        }

        return result.ToArray();
    }
}

public sealed record GetPagesQueryOut();

public sealed record GetPageQuery(string ID) : IQuery<Result<GetPageQueryOut>>, IGetQuery;
public sealed record GetPageQueryOut(PageVM PageVM);

public class PageVM
{
    public IdentityVM[] Path { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; init; } = Array.Empty<PropertyVM>();
    public IdentityVM[] Signatures { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM[] PracticeTactics { get; init; } = Array.Empty<IdentityVM>();
    public Style StyleOfPage { get; init; } = new();
    public Style[] StylesOfChildren { get; init; } = Array.Empty<Style>();

}

public class PropertyVM
{
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; init; } = Array.Empty<PropertyVM>();

    public PropertyType PropertyType { get; init; }

    public bool IsExpanded { get; init; } = true;
    public bool CanExpand { get; init; } = true;
    public bool HasChildren { get; init; }
}

public class IdentityVM
{
    public string ID { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public IdentityVM() {}
    public IdentityVM(string id, string name)
    {
        ID = id;
        Name = name;
    }

    public static implicit operator IdentityVM((string id, string name) args) => new IdentityVM(args.id, args.name);
}
