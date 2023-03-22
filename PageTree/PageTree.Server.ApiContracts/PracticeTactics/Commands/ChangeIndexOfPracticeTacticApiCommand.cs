using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class ChangeIndexOfPracticeTacticApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeTactic))]
        public string PracticeTacticID { get; set; }
        public int Index { get; set; }
    }
}
