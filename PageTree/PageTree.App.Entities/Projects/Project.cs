using Common.Basic.DDD;

namespace PageTree.Domain.Projects
{
    public class Project : Entity
    {
        public Project(string id, string rootPageID) : base(id)
        {
            RootPageID = rootPageID;
        }

        public string RootPageID { get; set; } = new("");
        public string Name { get; set; } = new("New Project");
        public string Description { get; set; } = new("");
    }
}
