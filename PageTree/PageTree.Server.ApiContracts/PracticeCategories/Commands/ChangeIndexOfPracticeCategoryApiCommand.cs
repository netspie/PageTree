using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class ChangeIndexOfPracticeCategoryApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeCategory))]
        public string PracticeCategoryID { get; set; }
        public int Index { get; set; }
    }
}
