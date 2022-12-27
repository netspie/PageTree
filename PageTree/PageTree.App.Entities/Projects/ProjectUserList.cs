using Common.Basic.DDD;

namespace PageTree.Domain.Projects
{
    public class ProjectUserList : Entity
    {
        public ProjectUserList() { }
        public ProjectUserList(string id) :base(id) {}
        public List<string> ProjectsCreatedIDs { get; set; } = new();
        public List<string> ProjectsArchivedIDs { get; set; } = new();
    }
}
