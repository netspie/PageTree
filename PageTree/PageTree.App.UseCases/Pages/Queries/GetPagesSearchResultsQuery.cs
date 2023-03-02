using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Corelibs.Basic.Collections;
using Corelibs.Basic.Searching;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;
using System.Web;

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

        var expression = "api/pages/search?type=start";
        
        var queryDict = HttpUtility.ParseQueryString(query.Expression);

        return res.With(new GetPagesSearchResultsQueryOut(
            new SearchedPagesResultsVM()
            {
               
            }
        ));
    }
}

public sealed record GetPagesSearchResultsQuery(string Expression) : IQuery<Result<GetPagesSearchResultsQueryOut>>;
public sealed record GetPagesSearchResultsQueryOut(SearchedPagesResultsVM PageVM);

public class SearchedPagesResultsVM
{
    public SearchedPageVM[] Values { get; set; }
}

public class SearchedPageVM
{
    public IdentityVM[] Path { get; init; }
    public IdentityVM Identity { get; init; }
    public IdentityVM SignatureIdentity { get; init; }
    public SearchedPageVM[] Properties { get; init; }
}

public class SearchedPagePropertyVM
{
    public IdentityVM Identity { get; init; }
    public IdentityVM SignatureIdentity { get; init; }
}
