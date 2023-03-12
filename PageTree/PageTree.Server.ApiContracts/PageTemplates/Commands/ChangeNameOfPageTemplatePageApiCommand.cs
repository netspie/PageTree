using Corelibs.Basic.Net;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class ChangeNameOfPageTemplatePageApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        public string Name { get; set; }
    }
}
