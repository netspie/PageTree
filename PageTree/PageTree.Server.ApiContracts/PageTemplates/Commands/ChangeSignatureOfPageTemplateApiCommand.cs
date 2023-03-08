using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeSignatureOfPageTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }
    }
}
