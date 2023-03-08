using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeIndexOfPropertyTemplateApiCommand
    {
        [AuthorizeResource(typeof(Page))]
        public string PageTemplateID { get; set; }

        [AuthorizeResource(typeof(Page))]
        public string PropertyTemplateID { get; set; }

        public int Index { get; set; }
    }
}
