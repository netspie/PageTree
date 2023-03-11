using PageTree.App.PageTemplates.Commands;
using PageTree.App.PageTemplates.Queries;

namespace PageTree.Server.ApiContracts
{
    public class PageTemplates_ApiToApp_MapProfile : BaseProfile
    {
        public PageTemplates_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetPageTemplatesApiQuery, GetPageTemplatesQuery>();
            CreateMapCirc<RemovePropertyTemplateApiCommand, RemovePropertyTemplateCommand>();
            CreateMapCirc<ChangeIndexOfPropertyTemplateApiCommand, ChangeIndexOfPropertyTemplateCommand>();
            CreateMapCirc<ChangeLevelOfPropertyTemplateApiCommand, ChangeLevelOfPropertyTemplateCommand>();
            CreateMapCirc<ChangeNameOfPageTemplateApiCommand, ChangeNameOfPageTemplateCommand>();
            CreateMapCirc<ChangeNameOfPageTemplatePageApiCommand, ChangeNameOfPageTemplatePageCommand>();
            CreateMapCirc<ChangeSignatureOfPageTemplateApiCommand, ChangeSignatureOfPageTemplateCommand>();
        }
    }
}
