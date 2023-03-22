using Common.Basic.Collections;
using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;
using Practicer.Domain.Pages.Common;

namespace PageTree.Domain.Practice
{
    public class PracticeTactic : Entity, IOwnedEntity
    {
        public PracticeTactic() {}
        public PracticeTactic(string id) : base(id) {}

        public PracticeTactic(string id, string name, string ownerID)
        {
            ID = id;
            Name = name;
            OwnerID = ownerID;
        }

        public PracticeTactic(string id, string name, string ownerID, string projectID) : this(id, name, ownerID) => ProjectID = projectID;
        public PracticeTactic(string id, string name, string ownerID, string projectID, string parentID) : this(id, name, ownerID, projectID) => ParentID = parentID;

        public string OwnerID { get; set; }
        public string ParentID { get; set; }
        public string ProjectID { get; set; }
        public List<string> ChildrenIDs { get; set; }

        public string Name { get; set; } = new("");
        public string PracticeCategoryID { get; set; }
        public List<PracticeTacticItem> PageItems { get; set; }
        public List<string> Items { get; set; }
        public List<string> SkipItemIfContainsOfIDs { get; set; }
        public List<string> SkipItemIfNotContainsOfIDs { get; set; }

        public bool CreatePracticeTactic(string id, int index)
        {
            if (ChildrenIDs == null)
                ChildrenIDs = new();

            return EditableItemOwnerFunctions_NoName.Create(ChildrenIDs, id, index);
        }

        public bool RemovePracticeTactic(string id) =>
            EditableItemOwnerFunctions.Remove(id, ChildrenIDs);

        public bool MovePracticeTactic(string id, int index) =>
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
