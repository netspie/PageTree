using AutoMapper;
using PageTree.App.ProjectUserLists.Queries;

namespace PageTree.Server.ApiContracts.Pages
{
    public class ProjectUserList_ApiToApp_MapProfile : Profile
    {
        public ProjectUserList_ApiToApp_MapProfile()
        {
            CreateMap<GetProjectUserListApiQuery, GetProjectUserListQuery>();
        }
    }
}
