using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Mediator;
using PageTree.App.Common;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;
using PageTree.Domain.Projects;

namespace PageTree.App.PageTemplates.Queries;

public class GetPageTemplatesQueryHandler : IQueryHandler<GetPageTemplatesQuery, Result<GetPageTemplatesQueryOut>>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<Signature> _signatureRepository;

    public GetPageTemplatesQueryHandler(
        IRepository<PageTemplate> pageTemplateRepository,
        IRepository<Project> projectRepository,
        IRepository<Signature> signatureRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
        _projectRepository = projectRepository;
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result<GetPageTemplatesQueryOut>> Handle(GetPageTemplatesQuery query, CancellationToken cancellationToken)
    {
        var res = Result<GetPageTemplatesQueryOut>.Success();

        var pageTemplate = await _pageTemplateRepository.Get(query.ID, res);
        var signature = await _signatureRepository.Get(pageTemplate.SignatureID, res);

        var project = await _projectRepository.Get(pageTemplate.ProjectID, res);
        var signaturesRoot = await _signatureRepository.Get(project.SignatureRootID, res);

        var parentPages = new List<PageTemplate>();
        res += await _pageTemplateRepository.GetParents(pageTemplate, p => p.ParentID, parentPages);
        parentPages.Reverse();

        var values = await GetProperties(pageTemplate);
        var signatures = await _signatureRepository.Get(signaturesRoot.ChildrenIDs, res);
        signatures = signatures.OrderBy(s => signaturesRoot.ChildrenIDs.IndexOf(s.ID)).ToArray();

        return res.With(new GetPageTemplatesQueryOut(
            new PageTemplatesVM()
            {
                ID = pageTemplate.ID,

                ProjectID = project.ID,

                Values = values,

                Signatures = signatures.Select(s => new IdentityVM(s.ID, s.Name)).ToArray(),
            }
        ));
    }

    public async Task<PageTemplateVM[]> GetProperties(PageTemplate currentPage)
    {
        var res = Result.Success();

        var result = new List<PageTemplateVM>();

        for (int i = 0; i < currentPage.ChildrenIDs.Count; i++)
        {
            var childID = currentPage.ChildrenIDs[i];

            var childPage = await _pageTemplateRepository.Get(childID, res);
            if (childPage == null)
                continue;

            var childSignature = !string.IsNullOrEmpty(childPage.SignatureID) ?
                await _signatureRepository.Get(childPage.SignatureID, res) :
                new Signature();

            var properties = Array.Empty<PageTemplateVM>();
            if (childPage.IsExpanded)
                properties = await GetProperties(childPage);

            result.Add(new PageTemplateVM()
            {
                TemplateName = childPage.TemplateName,
                Identity = (childPage.ID, childPage.Name),
                SignatureIdentity = (childSignature.ID, childSignature.Name),
                Properties = properties,
                HasChildren = childPage.ChildrenIDs.Count > 0,
                IsExpanded = childPage.IsExpanded
            });
        }

        return result.ToArray();
    }
}

public sealed record GetPageTemplatesQuery(string ID) : IQuery<Result<GetPageTemplatesQueryOut>>, IGetQuery;
public sealed record GetPageTemplatesQueryOut(PageTemplatesVM PageTemplates);

public class PageTemplatesVM
{
    public string ID { get; set; }
    public string ProjectID { get; init; }
    public PageTemplateVM[] Values { get; init; } = Array.Empty<PageTemplateVM>();
    public IdentityVM[] Signatures { get; init; } = Array.Empty<IdentityVM>();
}

public class PageTemplateVM
{
    public string TemplateName { get; init; }
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PageTemplateVM[] Properties { get; init; } = Array.Empty<PageTemplateVM>();
    public IdentityVM[] Signatures { get; init; } = Array.Empty<IdentityVM>();

    public bool IsExpanded { get; init; } = true;
    public bool CanExpand { get; init; } = true;
    public bool HasChildren { get; init; }
}
