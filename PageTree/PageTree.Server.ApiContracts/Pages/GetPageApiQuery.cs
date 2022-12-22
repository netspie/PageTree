using Microsoft.AspNetCore.Mvc;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetPageApiQuery
    {
        [FromRoute]
        public string ID { get; set; }
    }
}
