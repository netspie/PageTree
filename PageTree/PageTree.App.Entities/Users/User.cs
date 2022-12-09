using Common.Basic.DDD;

namespace PageTree.Domain.Users
{
    public class User : Entity
    {
        public List<string> ProjectIDs { get; set; } = new List<string>();
    }
}
