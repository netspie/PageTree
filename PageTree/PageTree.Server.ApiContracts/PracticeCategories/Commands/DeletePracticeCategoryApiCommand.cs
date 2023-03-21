using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class DeletePracticeCategoryApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeCategory))]
        public string PracticeCategoryID { get; set; }
    }
}
