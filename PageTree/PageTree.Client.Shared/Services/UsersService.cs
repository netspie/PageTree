using Corelibs.BlazorShared;
using PageTree.Server.ApiContracts.Users.Commands;

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
            var response = await _clientFactory.PostResource<CreateUserApiCommand>(_signInRedirector, "api/v1/users?action=create");
        }
    }
}
