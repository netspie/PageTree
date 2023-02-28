using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class RemovePropertyApiCommand
    {
        [AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string PropertyID { get; set; }
    }
}
