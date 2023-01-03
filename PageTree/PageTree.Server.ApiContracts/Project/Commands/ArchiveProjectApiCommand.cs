using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class ArchiveProjectApiCommand
    {
        [FromRoute]
        public string ID { get; set; }
        public string ProjectListID { get; set; }
    }
}
