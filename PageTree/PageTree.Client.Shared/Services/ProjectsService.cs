using Common.Basic.Collections;
using Corelibs.BlazorShared;
using PageTree.App.Projects.Queries;
using PageTree.App.ProjectUserLists.Queries;
using PageTree.Server.ApiContracts.Attributes;
using PageTree.Server.ApiContracts.Pages;
using PageTree.Server.ApiContracts.Project;

namespace PageTree.Client.Shared.Services
{
    public class ProjectsService : BaseService
    {
        public ProjectsService(IHttpClientFactory clientFactory, ISignInRedirector signInRedirector) 
            : base(clientFactory, signInRedirector)
        {
        }

        public async Task CreateProject()
        {
            var response = await PostResource("api/v1/projects?action=create");
        }

        public async Task<GetProjectOfIDQueryOut> GetProject(string id)
        {
            if (id.IsNullOrEmpty())
                return default;

            return await GetResource<GetProjectApiQuery, GetProjectOfIDQueryOut>(
                new GetProjectApiQuery(id), $"api/v1/projects/{id}/", typeof(FromRouteAttribute));
        }

        public async Task EditProject(string id, string name, string description)
        {
            var response = await PutResource($"api/v1/projects/{id}?action=edit",
                new EditProjectApiCommand(id, name, description));
        }
    }
}
