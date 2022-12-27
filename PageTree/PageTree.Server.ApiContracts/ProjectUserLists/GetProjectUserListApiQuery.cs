using PageTree.Server.ApiContracts.Attributes;

namespace PageTree.Server.ApiContracts.Pages
{
    public class GetProjectUserListApiQuery
    {
        [FromRoute]
        public string ID { get; set; }

        public GetProjectUserListApiQuery(string id)
        {
            ID = id;
        }
    }
}
