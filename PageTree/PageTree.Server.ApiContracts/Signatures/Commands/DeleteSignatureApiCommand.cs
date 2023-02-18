using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;

namespace PageTree.Server.ApiContracts
{
    public class DeleteSignatureApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }

        public DeleteSignatureApiCommand() {}
        public DeleteSignatureApiCommand(string signatureID)
        {
            SignatureID = signatureID;
        }
    }
}
