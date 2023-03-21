using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;

namespace PageTree.Server.ApiContracts
{
    public class DeleteSignatureApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }
    }
}
