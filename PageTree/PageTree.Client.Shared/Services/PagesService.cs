using Common.Basic.Blocks;
using Corelibs.BlazorShared;
using Mediator;

namespace PageTree.Client.Shared.Services
{
    internal class PagesService
    {

    }

    internal class UsersService : BaseService
    {
        private readonly IDataService _dataService;

        public Task CreateUser()
        {

        }
    }

    internal abstract class BaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISignInRedirector _signInRedirector;

        public BaseService(IHttpClientFactory clientFactory, ISignInRedirector signInRedirector)
        {
            _clientFactory = clientFactory;
            _signInRedirector = signInRedirector;
        }

        protected Task<TResponse> GetResource<TResponse>(IQuery<Result<TResponse>> query, string resourcePath, CancellationToken ct = default)
        {
            var queryString = query.GetQueryString();
            var resourcePathWithQuery = $"{resourcePath}?{queryString}";

            return _clientFactory.GetResource<TResponse> (_signInRedirector, resourcePathWithQuery, ct);
        }
    }
}
