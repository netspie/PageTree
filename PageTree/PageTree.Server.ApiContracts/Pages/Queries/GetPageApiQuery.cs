using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class GetPageApiQuery : IApiQuery
    {
        [FromRoute]
        public string ID { get; set; }

        public bool IsEditMode { get; set; } = false;
    }
}
