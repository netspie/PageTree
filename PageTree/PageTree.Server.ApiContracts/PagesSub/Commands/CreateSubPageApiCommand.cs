using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class CreateSubPageApiCommand
    {
        [FromRoute]
        public string ParentID { get; set; }
    }
}
