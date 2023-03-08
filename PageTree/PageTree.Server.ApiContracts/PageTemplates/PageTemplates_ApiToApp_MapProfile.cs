using PageTree.App.PageTemplates.Commands;

namespace PageTree.Server.ApiContracts
{
    public class PageTemplates_ApiToApp_MapProfile : BaseProfile
    {
        public PageTemplates_ApiToApp_MapProfile()
        {
            //CreateMapCirc<GetPageTemplateApiQuery, GetPageQuery>();
            CreateMapCirc<RemovePropertyTemplateApiCommand, RemovePropertyTemplateCommand>();
            CreateMapCirc<ChangeIndexOfPropertyTemplateApiCommand, ChangeIndexOfPropertyTemplateCommand>();
            CreateMapCirc<ChangeLevelOfPropertyTemplateApiCommand, ChangeLevelOfPropertyTemplateCommand>();
            CreateMapCirc<ChangeNameOfPageTemplateApiCommand, ChangePageTemplateNameCommand>();
            CreateMapCirc<ChangeNameOfPageTemplatePageApiCommand, ChangePageTemplatePageNameCommand>();
            CreateMapCirc<ChangeSignatureOfPageTemplateApiCommand, ChangeSignatureOfPageTemplateCommand>();
        }
    }
}
