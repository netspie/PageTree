using Common.Basic.DDD;

namespace PageTree.Domain.Projects
{
    public class Project : Entity
    {
        public string RootPageID { get; set; } = new("");
        public string Description { get; set; } = new("");
    }
}
