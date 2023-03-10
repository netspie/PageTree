using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class GetPageTemplatesApiQuery : IApiQuery
    {
        [FromRoute]
        public string ID { get; set; }
    }
}
