using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Corelibs.Basic.Collections;
using Corelibs.Basic.Searching;
using Mediator;
using PageTree.App.Common;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;
using PageTree.App.UseCases.Pages.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Queries;

public class GetPagesSearchResultsQueryHandler : IQueryHandler<GetPagesSearchResultsQuery, Result<GetPagesSearchResultsQueryOut>>
{
    private readonly ISearchEngine<Page> _searchEngine;
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<Signature> _signatureRepository;

    public GetPagesSearchResultsQueryHandler(
        ISearchEngine<Page> searchEngine,
        IRepository<Page> pageRepository,
        IRepository<Signature> signatureRepository)
    {
        _searchEngine = searchEngine;
        _pageRepository = pageRepository;
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result<GetPagesSearchResultsQueryOut>> Handle(GetPagesSearchResultsQuery query, CancellationToken cancellationToken)
    {
        var res = Result<GetPagesSearchResultsQueryOut>.Success();

        var searchType = SearchType.Substring;
        var nameSplit = query.Name.Split(":");
        if (nameSplit.Length > 1)
            searchType = nameSplit[1].ToSearchType();

        var searchResults = await Task.Run(() =>
        {
            return _searchEngine.Search(nameSplit[0], searchType);
        });

        var resultsVMs = await searchResults.ToSearchedPageVMs(query.ProjectID, _pageRepository, _signatureRepository);
        var resultsVMsValue = resultsVMs.GetNestedValue<SearchedPageVM[]>();

        res += resultsVMs;

        return res.With(new GetPagesSearchResultsQueryOut(
            new SearchedPagesResultsVM()
            {
               Values = resultsVMsValue
            }
        ));
    }
}

public sealed record GetPagesSearchResultsQuery(string ProjectID, string Name) : IQuery<Result<GetPagesSearchResultsQueryOut>>;
public sealed record GetPagesSearchResultsQueryOut(SearchedPagesResultsVM PageVM);

public class SearchedPagesResultsVM
{
    public SearchedPageVM[] Values { get; set; } = Array.Empty<SearchedPageVM>();
}

public class SearchedPageVM
{
    public IdentityVM[] Path { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM Identity { get; init; } = new();
    public IdentityVM SignatureIdentity { get; init; } = new();
    public IdentityVM[] Properties { get; init; } = Array.Empty<IdentityVM>();
}

public class SearchedPagePropertyVM
{
    public IdentityVM Identity { get; init; } = new();
    public IdentityVM SignatureIdentity { get; init; } = new();
}

public static class SearchTypeExtensions
{
    public static SearchType ToSearchType(this string searchType) =>
        searchType switch
        {
            "start" => SearchType.Start,
            "end" => SearchType.End,
            "sub" => SearchType.Substring,
            "full" => SearchType.Full,
            _ => SearchType.Substring
        };
}

public static class SearchIndexDataExtensions
{
    public static async Task<Result<SearchedPageVM[]>> ToSearchedPageVMs(
        this IEnumerable<SearchIndexData> pageDatas,
        string projectID,
        IRepository<Page> pageRepository,
        IRepository<Signature> signatureRepository)
    {
        var res = Result<SearchedPageVM[]>.Success();
        var list = new List<SearchedPageVM>();

        foreach (var pageData in pageDatas)
        {
            var page = await pageRepository.Get(pageData.ID, res);
            if (page == null || !page.ID.IsID())
                continue;

            if (page.ProjectID != projectID)
                continue;

            if (page.ParentID.IsNullOrEmpty())
                continue;

            var signature = page.SignatureID != null ?
                await signatureRepository.Get(page.SignatureID, res) :
                null;

            var parentPages = new List<Page>();
            res += await pageRepository.GetParents(page, p => p.ParentID, parentPages);
            parentPages.Reverse();

            var childrenPages = await pageRepository.Get(page.ChildrenIDs, res);

            var vm = new SearchedPageVM()
            {
                Identity = new(page.ID, page.Name),
                SignatureIdentity = signature != null ? new(signature.ID, signature.Name) : null,
                Path = parentPages.ToIdentityVMs(),
                Properties = childrenPages.ToIdentityVMs(),
            };

            list.Add(vm);
        }

        return res.With(list.ToArray());
    }
}
