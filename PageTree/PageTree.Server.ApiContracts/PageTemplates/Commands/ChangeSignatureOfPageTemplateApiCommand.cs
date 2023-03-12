using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.Server.ApiContracts
{
    public class ChangeSignatureOfPageTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PageTemplate))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }
    }
}
