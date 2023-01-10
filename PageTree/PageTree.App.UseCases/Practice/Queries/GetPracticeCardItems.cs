using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using PageTree.App.Common;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;
using PageTree.Domain.Practice;

namespace PageTree.App.Practice.Queries;

public class GetPracticeCardItemsQueryHandler
{
    public IRepository<Page> _pageRepository { private get; init; }
    public IRepository<Signature> _signatureRepository { private get; init; }
    public IRepository<PracticeTactic> _practiceTacticRepository { private get; init; }

    public async Task<GetPracticeCardItemsQueryDTO> Handle(GetPracticeCardItemsQuery query)
    {
        var res = Result<GetPracticeCardItemsQueryDTO>.Success();

        var page = await _pageRepository.Get(query.PageID, res);
        var tactic = await _practiceTacticRepository.Get(query.PracticeTacticID, res);

        var cards = new List<PracticeCardDTO>();
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
                if (p.ContainsAnyNestedChildOfID(_pageRepository, tactic.SkipItemIfContainsOfIDs))
                    continue;

                if (!p.ContainsAll(_pageRepository, tactic.SkipItemIfNotContainsOfIDs))
                    continue;

                var dto = new PracticeCardDTO(p.ID);
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

        return new GetPracticeCardItemsQueryDTO(cards.ToArray());
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

        var children = await page.GetChildren(_pageRepository);
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
        var children = await currentPage.GetChildren(_pageRepository);
        foreach (var child in children)
        {
            if (child.SignatureID != null &&
                parentSignatureID != null &&
                child.SignatureID == parentSignatureID)
            {
                var childChildren = await child.GetChildren(_pageRepository);
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

public sealed record GetPracticeCardItemsQuery(string PageID, string PracticeTacticID);
public sealed record GetPracticeCardItemsQueryDTO(PracticeCardDTO[] Cards);
public sealed record PracticeCardDTO(string ID)
{
    public IList<string> Questions { get; } = new List<string>();
    public IList<string> Answers { get; } = new List<string>();
}


public static class PageExtensions
{
    public static async Task<Page[]> GetChildren(this Page page, IRepository<Page> pageRepository)
    {
        var results = await Task.WhenAll(page.ChildrenIDs.Select(id => pageRepository.GetBy(id)));
        return results.Select(r => r.Get()).ToArray();
    }
}
