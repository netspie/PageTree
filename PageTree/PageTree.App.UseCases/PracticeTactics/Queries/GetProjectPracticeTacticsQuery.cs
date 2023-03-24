using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Common.Basic.Threading;
using Mediator;
using Newtonsoft.Json.Linq;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;
using PageTree.App.Common;
using PageTree.App.UseCases.Signatures.Queries;

namespace PageTree.App.UseCases.PracticeTactics.Queries;

public class GetProjectPracticeTacticsQueryHandler : IQueryHandler<GetProjectPracticeTacticsQuery, Result<GetProjectPracticeTacticsQueryOut>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<PracticeTactic> _entityRepository;
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<Signature> _signatureRepository;

    public GetProjectPracticeTacticsQueryHandler(
        IRepository<Project> projectRepository,
        IRepository<PracticeTactic> entityRepository,
        IRepository<Page> pageRepository,
        IRepository<Signature> signatureRepository)
    {
        _projectRepository = projectRepository;
        _entityRepository = entityRepository;
        _pageRepository = pageRepository;
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result<GetProjectPracticeTacticsQueryOut>> Handle(GetProjectPracticeTacticsQuery query, CancellationToken ct)
    {
        var result = Result<GetProjectPracticeTacticsQueryOut>.Success();

        var project = await _projectRepository.Get(query.ProjectID, result);
        var entitiesRoot = await _entityRepository.Get(project.PracticeTacticRootID, result);

        var @out = new GetProjectPracticeTacticsQueryOut(
           new PracticeTacticsListVM() { RootID = entitiesRoot.ID });

        var childrenIDs = entitiesRoot.ChildrenIDs;
        if (childrenIDs.IsNullOrEmpty())
            return result.With(@out);

        var entities = await _entityRepository.Get(childrenIDs, result);
        entities = entities.OrderBy(s => childrenIDs.IndexOf(s.ID)).ToArray();

        var signaturesRoot = await _signatureRepository.Get(project.SignatureRootID, result);
        var signatures = await _signatureRepository.Get(signaturesRoot.ChildrenIDs, result);

        @out = new GetProjectPracticeTacticsQueryOut(
            new PracticeTacticsListVM()
            {
                RootID = entitiesRoot.ID,

                Signatures = signatures.Select(s => new IdentityVM(s.ID, s.Name)).ToArray(),

                Values = await entities.Select(async tactic => new PracticeTacticVM()
                {
                    Identity = (tactic.ID, tactic.Name),
                    PageItems = await tactic.PageItems.SelectOrDefault(async pageItem =>
                        new PageItemVM()
                        {
                            PageSignatures = await pageItem.PageSignaturesIDs.ToIdentityVM(_signatureRepository, p => p.Name, result),

                            QuestionsSignatures = await pageItem.QuestionsSignatureIDs.ToIdentityVM(_signatureRepository, p => p.Name, result),

                            AnswersSignatures = await pageItem.AnswersSignatureIDs.ToIdentityVM(_signatureRepository, p => p.Name, result),
                        }
                    ).Values(),

                    PagesToSkipIfContainsID = await tactic.SkipItemIfContainsOfIDs.ToIdentityVM(_pageRepository, p => p.Name, result),
                    
                    PagesToSkipIfNotContainsID = await tactic.SkipItemIfNotContainsOfIDs.ToIdentityVM(_pageRepository, p => p.Name, result)
                })
                .Values()
            });

        return result.With(@out);
    }
}

public sealed record GetProjectPracticeTacticsQuery(string ProjectID) : IQuery<Result<GetProjectPracticeTacticsQueryOut>>;
public sealed record GetProjectPracticeTacticsQueryOut(PracticeTacticsListVM SignatureList);

public class PracticeTacticsListVM
{
    public string RootID { get; set; } = "";
    public PracticeTacticVM[] Values { get; set; } = Array.Empty<PracticeTacticVM>();
    public IdentityVM[] Signatures { get; init; } = Array.Empty<IdentityVM>();
}

public class PracticeTacticVM
{
    public IdentityVM Identity { get; set; } = new();
    public PageItemVM[] PageItems { get; set; } = Array.Empty<PageItemVM>();
    public IdentityVM[] PagesToSkipIfContainsID { get; set; } = Array.Empty<IdentityVM>();
    public IdentityVM[] PagesToSkipIfNotContainsID { get; set; } = Array.Empty<IdentityVM>();
}

public class PageItemVM
{
    public IdentityVM[] PageSignatures { get; set; } = Array.Empty<IdentityVM>();
    public IdentityVM[] QuestionsSignatures { get; set; } = Array.Empty<IdentityVM>();
    public IdentityVM[] AnswersSignatures { get; set; } = Array.Empty<IdentityVM>();
}
