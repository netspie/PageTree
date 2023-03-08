using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class CreateSubPageTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageTemplateID { get; set; }
        public int Index { get; set; }
    }
}
