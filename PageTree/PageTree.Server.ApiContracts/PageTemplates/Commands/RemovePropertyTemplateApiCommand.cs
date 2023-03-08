using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class RemovePropertyTemplateApiCommand
    {
        [AuthorizeResource(typeof(Page))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string PropertyID { get; set; }
    }
}
