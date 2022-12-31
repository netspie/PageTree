using PageTree.App.ProjectUserLists.Queries;
using PageTree.Server.ApiContracts.Common;

namespace PageTree.Server.ApiContracts.Pages
{
    public class ProjectUserList_ApiToApp_MapProfile : BaseProfile
    {
        public ProjectUserList_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectUserListApiQuery, GetProjectUserListQuery>();
        }
    }
}
