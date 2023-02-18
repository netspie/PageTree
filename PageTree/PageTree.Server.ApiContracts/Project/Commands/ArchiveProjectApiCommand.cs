using Corelibs.Basic.Net;
using PageTree.Domain.Projects;

namespace PageTree.Server.ApiContracts
{
    public class ArchiveProjectApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Project))]
        public string ID { get; set; }
        public string ProjectListID { get; set; }
    }
}
