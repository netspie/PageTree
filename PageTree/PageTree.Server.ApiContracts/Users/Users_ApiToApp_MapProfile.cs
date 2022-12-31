using PageTree.App.UseCases.Users.Queries;
using PageTree.Server.ApiContracts.Common;
using PageTree.Server.ApiContracts.Project;

namespace PageTree.Server.ApiContracts.Projects
{
    public class Users_ApiToApp_MapProfile : BaseProfile
    {
        public Users_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetUserApiQuery, GetUserQuery>();
        }
    }
}
