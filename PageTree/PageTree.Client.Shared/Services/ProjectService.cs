using Corelibs.BlazorShared;

namespace PageTree.Client.Shared.Services
{
    public class ProjectService : BaseService
    {
        public ProjectService(IHttpClientFactory clientFactory, ISignInRedirector signInRedirector) 
            : base(clientFactory, signInRedirector)
        {
        }

        public async Task CreateProject()
        {
            var response = await PostResource("api/v1/projects?action=create");
        }
    }
}
