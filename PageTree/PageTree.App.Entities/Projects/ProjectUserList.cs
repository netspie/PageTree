using Common.Basic.Blocks;
using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;
using Corelibs.Basic.Collections;

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

        public bool ArchiveProject(string projectID)
        {
            if (!projectID.IsID())
                return false;

            if (!ProjectsCreatedIDs.Remove(projectID))
                return false;

            if (ProjectsArchivedIDs.Contains(projectID))
                return true;

            ProjectsArchivedIDs.Add(projectID);
            return true;
        }
    }
}
