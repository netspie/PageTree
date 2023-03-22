using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class ChangeNameOfPracticeTacticApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeTactic))]
        public string PracticeTacticID { get; set; }
        public string Name { get; set; }
    }
}
