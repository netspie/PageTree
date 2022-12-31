using Common.Basic.CQRS.Query;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts.Project
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
