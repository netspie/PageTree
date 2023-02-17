using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class GetProjectSignaturesApiQuery
    {
        [FromRoute]
        public string ProjectID { get; set; }

        public GetProjectSignaturesApiQuery() { }
        public GetProjectSignaturesApiQuery(string projectID)
        {
            ProjectID = projectID;
        }
    }
}
