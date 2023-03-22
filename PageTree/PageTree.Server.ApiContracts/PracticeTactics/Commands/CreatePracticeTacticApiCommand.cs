using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class CreatePracticeTacticApiCommand
    {
        [AuthorizeResource(typeof(CreatePracticeTacticApiCommand))]
        public string ParentID { get; set; }
        public int Index { get; set; }
    }
}
