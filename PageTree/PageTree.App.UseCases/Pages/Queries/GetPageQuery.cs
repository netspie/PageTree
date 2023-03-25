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
using PageTree.App.UseCases.Pages.Common;
using PageTree.App.UseCases.Common;
using PageTree.App.Common;
using PageTree.Domain.PageTemplates;
using Corelibs.Basic.Collections;

namespace PageTree.App.Pages.Queries;

public class GetPageQueryHandler : IQueryHandler<GetPageQuery, Result<GetPageQueryOut>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<Style> _styleRepository;
    private readonly IRepository<Signature> _signatureRepository;
    private readonly IRepository<PageTemplate> _templateRepository;
    private readonly IRepository<PracticeCategory> _practiceCategoryRepository;
    private readonly IRepository<PracticeTactic> _practiceTacticRepository;

    public GetPageQueryHandler(
        IRepository<Project> projectRepository,
        IRepository<Page> pageRepository,
        IRepository<Style> styleRepository,
        IRepository<Signature> signatureRepository,
        IRepository<PageTemplate> templateRepository,
        IRepository<PracticeCategory> practiceCategoryRepository,
        IRepository<PracticeTactic> practiceTacticRepository)
    {
        _projectRepository = projectRepository;
        _pageRepository = pageRepository;
        _styleRepository = styleRepository;
        _signatureRepository = signatureRepository;
        _templateRepository = templateRepository;
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

        var practiceCategories = await _practiceCategoryRepository.Get(practiceCategoryRoot.ChildrenIDs, res);
        var practiceTactics = await _practiceTacticRepository.Get(practiceTacticRoot.ChildrenIDs, res);

        var parentPages = new List<Page>();
        res += await _pageRepository.GetParents(page, p => p.ParentID, parentPages);
        parentPages.Reverse();

        IdentityVM[] templatesVM = null;
        if (query.IsEditMode)
        {
            var templatesRoot = await _templateRepository.Get(project.TemplatePageRootID, res);
            var templates = await _templateRepository.Get(templatesRoot.ChildrenIDs, res);
            templatesVM = templates.Select(s => new IdentityVM(s.ID, s.TemplateName)).ToArray();
        }
        else
        {
            bool isPublicRootSet = project.PublicRootPageID.IsID();
            if (isPublicRootSet)
            {
                bool doesPathContainPublicRoot = parentPages.FirstOrDefault(p => p.ID == project.PublicRootPageID) != null;
                if (!doesPathContainPublicRoot)
                {
                    page = await _pageRepository.Get(project.PublicRootPageID, res);
                    signature = await _signatureRepository.Get(page.SignatureID, res);

                    parentPages.Clear();
                    res += await _pageRepository.GetParents(page, p => p.ParentID, parentPages);
                    parentPages.Reverse();
                }

                var d = parentPages.Select((p, i) => new { p, i }).FirstOrDefault(d => d.p.ID == project.PublicRootPageID);
                if (d != null)
                    parentPages = parentPages.Skip(d.i).ToList();
                else
                    parentPages.Clear();
            }
        }

        //var projectStyle = await _styleRepository.Get(project?.StyleID, res);
        //var signatureStyles = await _styleRepository.Get(signature?.StyleIDs, res);
        //var pageStyle = await _styleRepository.Get(page?.StyleID, res);
        //var finalStyle = projectStyle?.Override(signatureStyles.Append(pageStyle).ToArray());

        var mainStyle = HardcodedStyles.GetMainStyle(
            new IdentityVM() { ID = page.ID, Name = page.Name },
            new IdentityVM() { ID = signature?.ID, Name = signature?.Name });

        var properties = await GetProperties(page, mainStyle?.TreeExpandInfo);
        var signatures = await _signatureRepository.Get(signaturesRoot.ChildrenIDs, res);
        signatures = signatures.OrderBy(s => signaturesRoot.ChildrenIDs.IndexOf(s.ID)).ToArray();

        return res.With(new GetPageQueryOut(
            new PageVM()
            {
                ProjectID = project.ID,

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
                Templates = templatesVM,

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
            if (childPage == null)
                continue;

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
                Properties = properties,
                PropertyType = currentPage.GetPropertyType(childPage.ID)
            });
        }

        return result.ToArray();
    }
}

public sealed record GetPageQuery(string ID, bool IsEditMode = false) : IQuery<Result<GetPageQueryOut>>, IGetQuery;
public sealed record GetPageQueryOut(PageVM PageVM);

public class PageVM
{
    public string ProjectID { get; init; }
    public IdentityVM[] Path { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; set; } = Array.Empty<PropertyVM>();
    public IdentityVM[] Signatures { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM[] Templates { get; init; } = Array.Empty<IdentityVM>();
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
