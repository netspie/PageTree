using Corelibs.Basic.Net;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class ChangeExpandOfPageTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        public bool IsExpanded { get; set; }
    }
}
