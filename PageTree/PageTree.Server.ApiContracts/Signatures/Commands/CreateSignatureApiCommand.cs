using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;

namespace PageTree.Server.ApiContracts
{
    public class CreateSignatureApiCommand
    {
        [AuthorizeResource(typeof(Signature))]
        public string ParentID { get; set; }
        public int Index { get; set; }

        public CreateSignatureApiCommand() {}
        public CreateSignatureApiCommand(string parentID, int index)
        {
            ParentID = parentID;
            Index = index;
        }
    }
}
