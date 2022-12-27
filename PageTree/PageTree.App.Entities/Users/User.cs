using Common.Basic.DDD;

namespace PageTree.Domain.Users
{
    public class User : Entity
    {
        public User(string id) : base(id) { }   

        public string ProjectSetID { get; set; } = new("");
    }
}
