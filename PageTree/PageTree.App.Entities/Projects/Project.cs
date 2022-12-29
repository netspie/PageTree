using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;

namespace PageTree.Domain.Projects
{
    public class Project : Entity, IOwnedEntity
    {
        public Project() {}
        public Project(string id, string rootPageID, string ownerID) : base(id)
        {
            RootPageID = rootPageID;
            OwnerID = ownerID;
        }

        public string RootPageID { get; set; } = new("");
        public string Name { get; set; } = new("New Project");
        public string Description { get; set; } = new("");
        public string OwnerID { get; set; } = new("");
    }
}
