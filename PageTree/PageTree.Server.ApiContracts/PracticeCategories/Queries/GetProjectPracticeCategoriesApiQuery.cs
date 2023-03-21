using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class GetProjectPracticeCategoriesApiQuery
    {
        [FromRoute]
        public string ProjectID { get; set; }
    }
}
