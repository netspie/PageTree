using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeLevelOfPropertyApiCommand
    {
        [AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string PropertyID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string NewPageID { get; set; }
    }
}
