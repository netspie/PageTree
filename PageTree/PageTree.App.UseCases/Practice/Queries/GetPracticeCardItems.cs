using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Common;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.Practice.Queries;

public class GetPracticeCardItemsQueryHandler : IQueryHandler<GetPracticeCardItemsQuery, Result<GetPracticeCardItemsQueryOut>>
{
    public readonly IRepository<Page> _pageRepository;
    public readonly IRepository<Signature> _signatureRepository;
    public readonly IRepository<PracticeTactic> _practiceTacticRepository;

    public GetPracticeCardItemsQueryHandler(
        IRepository<Page> pageRepository, 
        IRepository<Signature> signatureRepository,
        IRepository<PracticeTactic> practiceTacticRepository)
    {
        _pageRepository = pageRepository;
        _signatureRepository = signatureRepository;
        _practiceTacticRepository = practiceTacticRepository;
    }   

    public async ValueTask<Result<GetPracticeCardItemsQueryOut>> Handle(GetPracticeCardItemsQuery query, CancellationToken ct)
    {
        var res = Result<GetPracticeCardItemsQueryOut>.Success();

        var page = await _pageRepository.Get(query.PageID, res);
        var tactic = await _practiceTacticRepository.Get(query.PracticeTacticID, res);

        var cards = new List<PracticeCardVM>();
        foreach (var tacticItem in tactic.PageItems)
        {
            var resultPages = new List<Page>();

            await AddToResult(page, tacticItem.PageSignaturesParentsIDs.FirstOrDefault(), tacticItem.PageSignaturesIDs.FirstOrDefault(), resultPages, false);
            resultPages = resultPages.DistinctBy(i => i.ID).ToList();

            string GetName(string id, string name)
            {
                var modifiedName = name;// GetModifiedName(id, name);
                return modifiedName.IsNullOrEmpty() ? name : modifiedName;
            }

            foreach (var p in resultPages)
            {
                if (await p.ContainsAnyNestedChildOfID(_pageRepository, tactic.SkipItemIfContainsOfIDs))
                    continue;

                if (!await p.ContainsAll(_pageRepository, tactic.SkipItemIfNotContainsOfIDs))
                    continue;

                var dto = new PracticeCardVM(p.ID);
                var thisPage = await GetPage(dto.ID);
                if (!thisPage)
                    continue;

                string thisPageName = GetName(thisPage.ID, thisPage.Name);

                foreach (var qID in tacticItem.QuestionsSignatureIDs)
                {
                    var subPage = await GetNestedSubPageOrThisOfTemplateSignatureID(p, qID);
                    if (!thisPage || !subPage)
                        continue;

                    string subPageName = GetName(subPage.ID, subPage.Name);

                    if (p.SignatureID == qID)
                        dto.Questions.Add(thisPageName);
                    else
                        dto.Questions.Add(subPageName);
                }

                foreach (var aID in tacticItem.AnswersSignatureIDs)
                {
                    var subPage = await GetNestedSubPageOrThisOfTemplateSignatureID(p, aID);
                    if (!thisPage || !subPage)
                        continue;

                    string subPageName = GetName(subPage.ID, subPage.Name);

                    if (p.SignatureID == aID)
                        dto.Answers.Add(thisPageName);
                    else
                        dto.Answers.Add(subPageName);
                }

                if (dto.Answers.IsNullOrEmpty() || dto.Questions.IsNullOrEmpty())
                    continue;

                cards.Add(dto);
            }
        }

        return res.With(new GetPracticeCardItemsQueryOut(cards.ToArray()));
    }

    private async Task<Page> GetPage(string id)
    {
        var result = await _pageRepository.GetBy(id);
        return result.Get();
    }

    private async Task<Page> GetNestedSubPageOrThisOfTemplateSignatureID(Page page, string templateSignatureID)
    {
        if (page.SignatureID == templateSignatureID)
            return page;

        var res = Result.Success();
        var children = await _pageRepository.Get(page.ChildrenIDs, res);
        foreach (var child in children)
        {
            if (child.SignatureID == templateSignatureID)
                return child;

            if (!page.IsSubPage(child.ID))
                continue;

            var subPage = await GetNestedSubPageOrThisOfTemplateSignatureID(child, templateSignatureID);
            if (subPage)
                return subPage;
        }

        return null;
    }

    private async Task AddToResult(Page currentPage, string parentSignatureID, string signatureID, List<Page> result, bool wasLinkBefore)
    {
        var res = Result.Success();

        var children = await _pageRepository.Get(currentPage.ChildrenIDs, res);
        foreach (var child in children)
        {
            if (child.SignatureID != null &&
                parentSignatureID != null &&
                child.SignatureID == parentSignatureID)
            {
                var childChildren = await _pageRepository.Get(child.ChildrenIDs, res);
                foreach (var childrenChild in childChildren)
                {
                    if (childrenChild.SignatureID == signatureID)
                        result.Add(child);
                }

                return;
            }

            // If at least two links happened then stop to avoid infinite looping
            var isChildSubPage = currentPage.IsSubPage(child.ID);
            if (!isChildSubPage && wasLinkBefore)
                continue;

            if (child.SignatureID == signatureID)
            {
                result.Add(child);
                continue;
            }

            wasLinkBefore = !isChildSubPage || wasLinkBefore;
            await AddToResult(child, parentSignatureID, signatureID, result, wasLinkBefore);
        }
    }
}

public sealed record GetPracticeCardItemsQuery(string PageID, string PracticeTacticID) : IQuery<Result<GetPracticeCardItemsQueryOut>>;
public sealed record GetPracticeCardItemsQueryOut(PracticeCardVM[] Cards);
public sealed record PracticeCardVM(string ID)
{
    public List<string> Questions { get; set; } = new();
    public List<string> Answers { get; set; } = new();
}