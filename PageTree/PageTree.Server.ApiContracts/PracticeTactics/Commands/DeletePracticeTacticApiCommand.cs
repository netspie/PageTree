using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class DeletePracticeTacticApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeTactic))]
        public string PracticeTacticID { get; set; }
    }
}
