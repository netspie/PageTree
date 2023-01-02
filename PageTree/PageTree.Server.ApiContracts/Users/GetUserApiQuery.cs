using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class GetUserApiQuery : IApiQuery
    {
        [FromRoute]
        public string ID { get; set; }

        public GetUserApiQuery() { }
        public GetUserApiQuery(string id)
        {
            ID = id;
        }
    }
}
