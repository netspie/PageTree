using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;

namespace PageTree.Server.ApiContracts
{
    public class ChangeIndexOfSignatureApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }
        public int Index { get; set; }
    }
}
