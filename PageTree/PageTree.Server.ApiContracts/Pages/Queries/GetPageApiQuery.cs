using Common.Basic.CQRS.Query;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetPageApiQuery : IApiQuery
    {
        [FromRoute]
        public string ID { get; set; }
    }
}
