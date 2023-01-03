using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class UpdatePageApiCommand
    {
        [FromRoute]
        public string PageID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
    }
}
