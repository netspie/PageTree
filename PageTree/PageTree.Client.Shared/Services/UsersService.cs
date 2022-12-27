using Corelibs.BlazorShared;
using PageTree.App.UseCases.Users.Queries;

namespace PageTree.Client.Shared.Services
{
    public class UsersService : BaseService
    {
        public UsersService(IHttpClientFactory clientFactory, ISignInRedirector signInRedirector) 
            : base(clientFactory, signInRedirector)
        {
        }

        public async Task CreateUser()
        {
            var response = await PostResource("api/v1/users?action=create");
        }

        public Task<GetUserQueryOut> GetUser()
        {
            return _clientFactory.GetResource<GetUserQueryOut>(_signInRedirector, "api/v1/users/me");
        }
    }
}
