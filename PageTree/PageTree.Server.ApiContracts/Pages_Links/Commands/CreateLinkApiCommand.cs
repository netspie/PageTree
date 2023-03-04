using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class CreateLinkApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string LinkID { get; set; }

        public int Index { get; set; }
    }
}
