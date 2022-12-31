using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetPagesApiQuery
    {
        [FromRoute]
        public string Name { get; set; }
    }
}
