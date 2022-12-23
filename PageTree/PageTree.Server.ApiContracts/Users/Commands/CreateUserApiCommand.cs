using PageTree.Server.ApiContracts.Attributes;

namespace PageTree.Server.ApiContracts.Users.Commands
{
    public class CreateUserApiCommand
    {
        [FromRoute]
        public string ID { get; set; }
    }
}
