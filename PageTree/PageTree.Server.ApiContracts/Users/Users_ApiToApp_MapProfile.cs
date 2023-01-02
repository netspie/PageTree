using PageTree.App.UseCases.Users.Queries;

namespace PageTree.Server.ApiContracts
{
    public class Users_ApiToApp_MapProfile : BaseProfile
    {
        public Users_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetUserApiQuery, GetUserQuery>();
        }
    }
}
