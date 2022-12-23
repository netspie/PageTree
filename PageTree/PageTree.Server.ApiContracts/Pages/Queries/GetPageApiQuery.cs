using PageTree.Server.ApiContracts.Attributes;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetPageApiQuery
    {
        [FromRoute]
        public string ID { get; set; }
    }
}
