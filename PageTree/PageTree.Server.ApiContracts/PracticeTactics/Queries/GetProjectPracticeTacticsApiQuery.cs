using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class GetProjectPracticeTacticsApiQuery
    {
        [FromRoute]
        public string ProjectID { get; set; }
    }
}
