using PageTree.Server.ApiContracts.Attributes;

namespace PageTree.Server.ApiContracts.Project
{
    public class GetProjectApiQuery
    {
        [FromRoute]
        public string ID { get; set; }

        public GetProjectApiQuery() { }
        public GetProjectApiQuery(string id)
        {
            ID = id;
        }
    }
}
