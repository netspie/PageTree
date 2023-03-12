using Corelibs.Basic.Net;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class ChangeLevelOfPropertyTemplateApiCommand
    {
        [AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(PageTemplate))]
        public string PropertyTemplateID { get; set; }

        [AuthorizeResource(typeof(PageTemplate))]
        public string NewPageID { get; set; }
    }
}
