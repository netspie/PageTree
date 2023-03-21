using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class ChangeNameOfPracticeCategoryApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeCategory))]
        public string PracticeCategoryID { get; set; }
        public string Name { get; set; }
    }
}
