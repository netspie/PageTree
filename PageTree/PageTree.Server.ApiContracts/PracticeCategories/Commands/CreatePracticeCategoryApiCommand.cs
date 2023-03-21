using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class CreatePracticeCategoryApiCommand
    {
        [AuthorizeResource(typeof(PracticeCategory))]
        public string ParentID { get; set; }
        public int Index { get; set; }
    }
}
