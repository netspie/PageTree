using Corelibs.Basic.Net;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class RemovePropertyTemplateApiCommand
    {
        [AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(PageTemplate))]
        public string PropertyID { get; set; }
    }
}
