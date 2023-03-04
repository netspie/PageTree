using Common.Basic.Collections;
using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;
using Corelibs.Basic.Collections;
using Practicer.Domain.Pages.Common;

namespace PageTree.Domain
{
    public class Page : Entity, IOwnedEntity
    {
        public Page() {}
        public Page(string id) : base(id) { }
        public Page(string id, string name) : this(id) { Name = name; }
        public Page(string id, string name, string ownerID) : this(id, name) { OwnerID = ownerID; }
        public Page(string id, string name, string ownerID, string projectID) : this(id, name, ownerID) 
            { ProjectID = projectID; }

        public bool IsSubPage(string id) =>
            SubPages.FirstOrDefault(sID => sID == id) != null;

        public bool CreateSubPage(string id, int index)
        {
            if (!id.IsID())
                return false;

            if (id == ID)
                return false;

            return EditableItemOwnerFunctions_NoName.Create(id, index, ChildrenIDs, SubPages);
        }

        public bool CreateLink(string id, int index)
        {
            if (!id.IsID())
                return false;

            if (id == ID)
                return false;

            return EditableItemOwnerFunctions_NoName.Create(id, index, ChildrenIDs, Links);
        }

        public bool Rename(string newName) =>
           EditableItemFunctions.Rename(newName, () => Name = newName);

        public bool Resignature(string signatureID)
        {
            if (!signatureID.IsID())
                return false;

            SignatureID = signatureID;

            return true;
        }

        public bool ReorderProperty(string id, int index) =>
            EditableItemOwnerFunctions.Reorder(id, index, ChildrenIDs);

        public bool RemoveProperty(string id)
        {
            if (Links.Any(lID => lID == id))
                return EditableItemOwnerFunctions.Remove(id, ChildrenIDs, Links);

            if (SubPages.Any(sID => sID == id))
                return EditableItemOwnerFunctions.Remove(id, ChildrenIDs, SubPages);

            return false;
        }

        public string OwnerID { get; set; } = new("");
        public string ProjectID { get; set; } = new("");
        public string Name { get; set; } = "New Page";

        public string SignatureID { get; set; } = new("");

        public string ParentID { get; set; } = new("");

        /// <summary>
        /// Ordered property ids. Contains of all of types of properties, like subpages, links etc.
        /// </summary>
        public List<string> ChildrenIDs { get; init; } = new();

        /// <summary>
        /// Part of children ids. Subpage can have only one parent.
        /// </summary>
        public List<string> SubPages { get; private set; } = new();

        /// <summary>
        /// Part of children ids. Link to another page.
        /// </summary>
        public List<string> Links { get; private set; } = new();

        /// <summary>
        /// Should be independent of styling? I don't think so... at least partially
        /// Maybe flag.. style-defined < user-modified
        /// </summary>
        public string ExpandInfoID { get; init; } = new("");

        /// <summary>
        /// Style applied for a unique page overrides all other styles applied (project, signature)
        /// </summary>
        public string StyleID { get; init; } = new("");

        /// <summary>
        /// Localized, sorting & filtering, styling, hidden items, hidden item types (links.. pseudo-properties), alternative content version,
        /// Suggested new versions..
        /// Allow to create versions groups... have some predefined ones (above) - change history in it as well, even multiple ones?
        /// </summary>
        public List<string> VersionsIDs { get; init; } = new();

        /// <summary>
        /// A id of versions of the page which will be public to everyone. If not exist, then default is core version.
        /// </summary>
        public List<string> PublishedVersionIDs { get; set; } = new();

        /// <summary>
        /// List of n previous changes, so user can go back anytime he wants
        /// Events! Expanded.. Collapsed, Changed bg Color, ChangedToStyle, NameChanged, 
        /// Group events?!
        /// External & internal events separately
        /// State & Style events events separately
        /// Global history and internal history - changed property name? keep it here and there (there in separate change history page versions, so user can go back from in page in different ways)
        /// Seems real heavy on coding.. is there a way to skip? 
        /// Just store json versions...
        /// </summary>
        public List<string> ChangeHistoryID { get; init; } = new();

        /// <summary>
        /// Id of this page, but already processed by styling - sorting, filtering and child content.. anything
        /// For optimization purposes, so query handler does not have to work much later, just fetch
        /// </summary>
        public string BakedPageID { get; init; } = new("");

        /// <summary>
        /// Ids of children considered private.
        /// They shouldn't be presented to any users.
        /// </summary>
        public List<string>? PrivateChildrenIDs { get; set; }
    }
}
