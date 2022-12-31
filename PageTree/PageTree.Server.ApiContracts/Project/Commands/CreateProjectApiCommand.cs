using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts.Project
{
    public class CreateProjectApiCommand
    {
        [FromRoute]
        public string ProjectUserListID { get; set; }

        public CreateProjectApiCommand() {}
    }
}
