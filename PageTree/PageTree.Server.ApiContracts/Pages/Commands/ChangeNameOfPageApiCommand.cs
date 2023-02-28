using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeNameOfPageApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }

        public string Name { get; set; }
    }
}
