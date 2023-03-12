using Corelibs.Basic.Net;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class ChangeIndexOfPropertyTemplateApiCommand
    {
        [AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(PageTemplate))]
        public string PropertyTemplateID { get; set; }

        public int Index { get; set; }
    }
}
