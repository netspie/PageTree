using PageTree.App.Pages.Commands;
using PageTree.App.Pages.Queries;

namespace PageTree.Server.ApiContracts
{
    public class Pages_ApiToApp_MapProfile : BaseProfile
    {
        public Pages_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetPageApiQuery, GetPageQuery>();
            CreateMapCirc<UpdatePageApiCommand, UpdatePageCommand>();
            CreateMapCirc<RemovePropertyApiCommand, RemovePropertyCommand>();
            CreateMapCirc<ChangeIndexOfPageApiCommand, ChangeIndexOfPageCommand>();
        }
    }
}
