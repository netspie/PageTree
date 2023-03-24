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
using Corelibs.Basic.Reflection;

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
        signatures = signatures.OrderBy(s => signaturesRoot.ChildrenIDs.IndexOf(s.ID)).ToArray();

        @out = new GetProjectPracticeTacticsQueryOut(
            new PracticeTacticsListVM()
            {
                ProjectID = query.ProjectID,
                RootID = entitiesRoot.ID,
                Signatures = signatures.Select(s => new IdentityVM(s.ID, s.Name)).ToArray(),
            });

        var values = new List<PracticeTacticVM>();
        foreach (var tactic in entities)
        {
            var tacticVM = new PracticeTacticVM();
            tacticVM.Identity = (tactic.ID, tactic.Name);

            var pageItems = new List<PageItemVM>();
            if (!tactic.PageItems.IsNullOrEmpty())
                foreach (var pageItem in tactic.PageItems)
                {
                    var pageItemVM = new PageItemVM();

                    var pageSignatures = new List<IdentityVM>();
                    if (!pageItem.PageSignaturesIDs.IsNullOrEmpty())
                        foreach (var signatureID in pageItem.PageSignaturesIDs)
                        {
                            var sig = await _signatureRepository.Get(signatureID, result);
                            pageSignatures.Add(new(signatureID, sig?.Name ?? "---"));
                        }
                    pageItemVM.PageSignatures = pageSignatures.ToArray();

                    var questionSignatures = new List<IdentityVM>();
                    if (!pageItem.QuestionsSignatureIDs.IsNullOrEmpty())
                        foreach (var signatureID in pageItem.QuestionsSignatureIDs)
                        {
                            var sig = await _signatureRepository.Get(signatureID, result);
                            questionSignatures.Add(new(signatureID, sig?.Name ?? "---"));
                        }
                    pageItemVM.QuestionsSignatures = questionSignatures.ToArray();

                    var answerSignatures = new List<IdentityVM>();
                    if (!pageItem.AnswersSignatureIDs.IsNullOrEmpty())
                        foreach (var signatureID in pageItem.AnswersSignatureIDs)
                        {
                            var sig = await _signatureRepository.Get(signatureID, result);
                            answerSignatures.Add(new(signatureID, sig?.Name ?? "---"));
                        }
                    pageItemVM.AnswersSignatures = answerSignatures.ToArray();

                    pageItems.Add(pageItemVM);
                }
            tacticVM.PageItems = pageItems.ToArray();

            var skipItems = new List<IdentityVM>();
            if (!tactic.SkipItemIfContainsOfIDs.IsNullOrEmpty())
                foreach (var itemID in tactic.SkipItemIfContainsOfIDs)
                {
                    var page = await _pageRepository.Get(itemID, result);
                    skipItems.Add(new(itemID, page?.Name ?? "---"));
                }
            tacticVM.PagesToSkipIfContainsID = skipItems.ToArray();

            var skipNotItems = new List<IdentityVM>();
            if (!tactic.SkipItemIfNotContainsOfIDs.IsNullOrEmpty())
                foreach (var itemID in tactic.SkipItemIfNotContainsOfIDs)
                {
                    var page = await _pageRepository.Get(itemID, result);
                    skipNotItems.Add(new(itemID, page?.Name ?? "---"));
                }
            tacticVM.PagesToSkipIfNotContainsID = skipNotItems.ToArray();

            values.Add(tacticVM);
        }

        @out.VM.Values = values.ToArray();

        return result.With(@out);
    }
}

public sealed record GetProjectPracticeTacticsQuery(string ProjectID) : IQuery<Result<GetProjectPracticeTacticsQueryOut>>;
public sealed record GetProjectPracticeTacticsQueryOut(PracticeTacticsListVM VM);

public class PracticeTacticsListVM
{
    public string ProjectID { get; set; } = "";
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
