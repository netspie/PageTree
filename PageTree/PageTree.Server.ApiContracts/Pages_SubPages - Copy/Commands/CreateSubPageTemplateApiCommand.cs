using Corelibs.Basic.Net;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class CreateSubPageTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }
        public int Index { get; set; }
    }
}
