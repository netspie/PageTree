using Common.Basic.DDD;

namespace PageTree.Domain.Projects
{
    public class ProjectSet : Entity
    {
        public List<string> ProjectsCreatedIDs { get; set; } = new();
    }
}
