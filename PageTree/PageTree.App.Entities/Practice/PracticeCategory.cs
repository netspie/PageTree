using Common.Basic.Collections;
using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;
using Practicer.Domain.Pages.Common;

namespace PageTree.Domain.Practice
{
    public class PracticeCategory : Entity, IOwnedEntity
    {
        public string Name { get; set; } = new("");
        public List<string> Items { get; set; } = new();
        public List<string> ChildrenIDs { get; set; }

        public string OwnerID { get; set; } = new("");
        public string ParentID { get; set; } = new("");
        public string ProjectID { get; set; } = new("");

        public PracticeCategory() { }

        public PracticeCategory(string id, string name, string ownerID)
        {
            ID = id;
            Name = name;
            OwnerID = ownerID;
        }

        public PracticeCategory(string id, string name, string ownerID, string projectID) : this(id, name, ownerID) => ProjectID = projectID;
        public PracticeCategory(string id, string name, string ownerID, string projectID, string parentID) : this(id, name, ownerID, projectID) => ParentID = parentID;

        public bool CreatePracticeCategory(string id, int index) =>
            EditableItemOwnerFunctions_NoName.Create(ChildrenIDs, id, index);

        public bool RemovePracticeCategory(string id) =>
            EditableItemOwnerFunctions.Remove(id, ChildrenIDs);

        public bool MovePracticeCategory(string id, int index) =>
            EditableItemOwnerFunctions.Reorder(id, index, ChildrenIDs);

        public bool Rename(string name)
        {
            if (name.IsNullOrEmpty())
                return false;

            Name = name;
            return true;
        }
    }
}
