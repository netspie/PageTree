using AutoMapper;
using Corelibs.BlazorShared;
using PageTree.App.Projects.Commands;
using PageTree.App.UseCases.Users.Commands;
using PageTree.Server.ApiContracts.Project;

namespace PageTree.Client.Shared.Services
{
    public class PageTreeCommandExecutor : CommandRequestExecutor
    {
        public PageTreeCommandExecutor(IMapper mapper, IHttpClientFactory clientFactory, ISignInRedirector signInRedirector)
            : base("/api/v1", mapper, clientFactory, signInRedirector)
        {
            AddPost<CreateUserCommand>("users");
            AddPost<CreateProjectCommand, CreateProjectApiCommand>("projects");
            AddPut<EditProjectCommand, EditProjectApiCommand>("projects");
        }
    }
}
