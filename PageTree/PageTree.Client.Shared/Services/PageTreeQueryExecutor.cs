using AutoMapper;
using Common.Basic.Collections;
using Corelibs.BlazorShared;
using PageTree.App.Projects.Queries;
using PageTree.App.ProjectUserLists.Queries;
using PageTree.App.UseCases.Users.Queries;
using PageTree.Server.ApiContracts.Pages;
using PageTree.Server.ApiContracts.Project;

namespace PageTree.Client.Shared.Services
{
    public class PageTreeQueryExecutor : QueryRequestExecutor
    {
        public PageTreeQueryExecutor(IMapper mapper, IHttpClientFactory clientFactory, ISignInRedirector signInRedirector)
            : base(mapper, clientFactory, signInRedirector)
        {
            Add<GetUserQuery, GetUserQueryOut>(q =>
            {
                if (q.ID.IsNullOrEmpty())
                    return GetResource(q, $"{_baseRoute}/users/me");
                
                return GetResource<GetUserQueryOut, GetUserApiQuery>(q, $"{_baseRoute}/users/{q.ID}");
            });

            Add<GetProjectUserListApiQuery, GetProjectUserListQuery, GetProjectUserListQueryOut>("projectsUserLists");
            Add<GetProjectApiQuery, GetProjectQuery, GetProjectQueryOut>("projects");
        }
    }
}
