using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class CreateSubPageApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }
        public int Index { get; set; }
    }
}
