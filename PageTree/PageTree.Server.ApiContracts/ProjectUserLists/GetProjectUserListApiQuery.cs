using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetProjectUserListApiQuery : IApiQuery
    {
        [FromRoute]
        public string ID { get; set; }

        public GetProjectUserListApiQuery() {}
        public GetProjectUserListApiQuery(string id)
        {
            ID = id;
        }
    }
}
