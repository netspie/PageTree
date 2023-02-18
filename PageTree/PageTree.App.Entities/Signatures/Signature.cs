using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;
using Practicer.Domain.Pages.Common;

namespace PageTree.App.Entities.Signatures
{
    public class Signature : Entity, IOwnedEntity
    {
        public Signature() {}

        public Signature(string id, string name, string ownerID)
        {
            ID = id;
            Name = name;
            OwnerID = ownerID;
        }

        public Signature(string id, string name, string ownerID, string projectID) : this(id, name, ownerID) => ProjectID = projectID;
        public Signature(string id, string name, string ownerID, string projectID, string parentID) : this(id, name, ownerID, projectID) => ParentID = parentID;

        public string Name { get; set; } = new("");
        public string ParentID { get; set; } = new("");
        public string ProjectID { get; set; } = new("");

        public List<string> ChildrenIDs { get; set; } = new();
        public List<string> StyleIDs { get; set; } = new();

        public string OwnerID { get; set; } = new("");

        public bool CreateSignature(string id, int index) =>
            EditableItemOwnerFunctions_NoName.Create(ChildrenIDs, id, index);

        public bool RemoveSignature(string id) =>
            EditableItemOwnerFunctions.Remove(id, ChildrenIDs);
    }
}
