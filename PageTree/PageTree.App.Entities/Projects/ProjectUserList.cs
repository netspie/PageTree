using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;

namespace PageTree.Domain.Projects
{
    public class ProjectUserList : Entity, IOwnedEntity
    {
        public ProjectUserList() { }
        public ProjectUserList(string id, string ownerID) :base(id) 
        {
            OwnerID = ownerID;
        }

        public List<string> ProjectsCreatedIDs { get; set; } = new();
        public List<string> ProjectsArchivedIDs { get; set; } = new();

        public string OwnerID { get; set; }
    }
}
