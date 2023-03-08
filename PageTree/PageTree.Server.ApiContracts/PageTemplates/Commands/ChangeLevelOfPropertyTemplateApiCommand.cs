using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeLevelOfPropertyTemplateApiCommand
    {
        [AuthorizeResource(typeof(Page))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string PropertyTemplateID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string NewPageID { get; set; }
    }
}
