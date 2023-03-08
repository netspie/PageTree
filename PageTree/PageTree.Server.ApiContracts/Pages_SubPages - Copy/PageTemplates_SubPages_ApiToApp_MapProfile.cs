using PageTree.App.PageTemplates.Commands;

namespace PageTree.Server.ApiContracts
{
    public class PageTemplates_SubPages_ApiToApp_MapProfile : BaseProfile
    {
        public PageTemplates_SubPages_ApiToApp_MapProfile()
        {
            CreateMapCirc<CreateSubPageTemplateApiCommand, CreateSubPageTemplateCommand>();
        }
    }
}
