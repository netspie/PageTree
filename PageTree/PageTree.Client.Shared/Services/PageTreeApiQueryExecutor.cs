using Common.Basic.Blocks;
using Corelibs.Basic.Collections;
using Corelibs.BlazorShared;
using Mediator;
using PageTree.App.Pages.Queries;

namespace PageTree.Client.Shared.Services
{
    internal class PageTreeApiQueryExecutor : IQueryExecutor
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISignInRedirector _signInRedirector;

        public PageTreeApiQueryExecutor(IHttpClientFactory clientFactory, ISignInRedirector signInRedirector)
        {
            _clientFactory = clientFactory;
            _signInRedirector = signInRedirector;
        }

        public Task<TResponse> Execute<TResponse>(IQuery<Result<TResponse>> query, CancellationToken ct = default)
        {
            var type = query.GetType();
            if (query is GetPageOfIDQuery)
                return GetResource(query, "api/pages", ct);

            if (query is GetPagesQuery)
                return GetResource(query, "api/pages", ct);

            throw new NotImplementedException();
        }

        private Task<TResponse> GetResource<TResponse>(IQuery<Result<TResponse>> query, string resourcePath, CancellationToken ct = default)
        {
            var queryString = query.GetQueryString();
            var resourcePathWithQuery = $"{resourcePath}?{queryString}";

            return _clientFactory.GetResource<TResponse>(_signInRedirector, resourcePathWithQuery, ct);
        }
    }
}
