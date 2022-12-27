using Common.Basic.Collections;
using Corelibs.BlazorShared;
using PageTree.App.ProjectUserLists.Queries;
using PageTree.Server.ApiContracts.Pages;

namespace PageTree.Client.Shared.Services
{
    public class ProjectUserListsService : BaseService
    {
        public ProjectUserListsService(IHttpClientFactory clientFactory, ISignInRedirector signInRedirector) 
            : base(clientFactory, signInRedirector)
        {
        }

        public async Task<GetProjectUserListQueryOut> GetProjectUserList(string id)
        {
            if (id.IsNullOrEmpty())
                return default;

            return await GetResource<GetProjectUserListApiQuery, GetProjectUserListQueryOut>(new GetProjectUserListApiQuery(id), "api/v1/projectUserLists");
        }
    }
}
