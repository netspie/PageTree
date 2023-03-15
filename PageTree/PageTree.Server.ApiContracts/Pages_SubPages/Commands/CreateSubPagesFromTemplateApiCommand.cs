using Corelibs.Basic.Net;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class CreateSubPagesFromTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }

        [AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        public int Index { get; set; }
    }
}
