using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class CreateSignatureApiCommand
    {
        [FromRoute]
        public string ProjectID { get; set; }

        public int Index { get; set; }

        public CreateSignatureApiCommand() {}
        public CreateSignatureApiCommand(string projectID, int index)
        {
            ProjectID = projectID;
            Index = index;
        }
    }
}
