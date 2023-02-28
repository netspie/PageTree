using PageTree.App.Pages.Queries;
using PageTree.App.Projects.Commands;

namespace PageTree.Server.ApiContracts
{
    public class Pages_ApiToApp_MapProfile : BaseProfile
    {
        public Pages_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetPageApiQuery, GetPageQuery>();
            CreateMapCirc<UpdatePageApiCommand, UpdatePageCommand>();
            CreateMapCirc<RemovePropertyApiCommand, RemovePropertyCommand>();
        }
    }
}
