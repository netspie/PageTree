using Common.Basic.CQRS.Query;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts.Project
{
    public class GetProjectApiQuery : IApiQuery
    {
        [FromRoute]
        public string ID { get; set; }

        public GetProjectApiQuery() { }
        public GetProjectApiQuery(string id)
        {
            ID = id;
        }
    }
}
