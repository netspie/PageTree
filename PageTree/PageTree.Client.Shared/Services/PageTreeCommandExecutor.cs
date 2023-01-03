using AutoMapper;
using Corelibs.BlazorShared;
using PageTree.App.Projects.Commands;
using PageTree.App.UseCases.Users.Commands;
using PageTree.Server.ApiContracts;

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

            AddPost<CreateSubPageCommand, CreateSubPageApiCommand>("pages/{parentID}/subpages");
            AddPatch<UpdatePageCommand, UpdatePageApiCommand>("pages/{pageID}");
        }
    }
}
