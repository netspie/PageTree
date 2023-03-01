using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeSignatureOfPageApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageID { get; set; }

        [AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }
    }
}
