using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class CreatePracticeTacticApiCommand
    {
        [AuthorizeResource(typeof(PracticeTactic))]
        public string ParentID { get; set; }
        public int Index { get; set; }
    }
}
