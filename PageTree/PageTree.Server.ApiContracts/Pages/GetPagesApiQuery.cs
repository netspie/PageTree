using Microsoft.AspNetCore.Mvc;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetPagesApiQuery
    {
        [FromRoute]
        public string Name { get; set; }
    }
}
