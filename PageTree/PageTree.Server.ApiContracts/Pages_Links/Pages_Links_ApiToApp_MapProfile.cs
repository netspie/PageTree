using PageTree.App.Pages.Commands;

namespace PageTree.Server.ApiContracts
{
    public class Pages_Links_ApiToApp_MapProfile : BaseProfile
    {
        public Pages_Links_ApiToApp_MapProfile()
        {
            CreateMapCirc<CreateLinkApiCommand, CreateLinkCommand>();
        }
    }
}
