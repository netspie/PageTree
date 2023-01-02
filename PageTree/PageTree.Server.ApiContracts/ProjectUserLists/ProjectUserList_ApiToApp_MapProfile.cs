using PageTree.App.ProjectUserLists.Queries;

namespace PageTree.Server.ApiContracts
{
    public class ProjectUserList_ApiToApp_MapProfile : BaseProfile
    {
        public ProjectUserList_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectUserListApiQuery, GetProjectUserListQuery>();
        }
    }
}
