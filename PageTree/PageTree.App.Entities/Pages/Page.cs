using Common.Basic.DDD;

namespace PageTree.Domain
{
    public class Page : Entity
    {
        public Page(string id) : base(id) {}

        public string AuthorID { get; init; } = new("");
        public string Name { get; init; } = string.Empty;

        public string SignatureID { get; init; } = new("");

        public string ParentID { get; init; } = new("");
        public List<string> ChildrenIDs { get; init; } = new();

        /// <summary>
        /// Should be independent of styling? I don't think so... at least partially
        /// Maybe flag.. style-defined < user-modified
        /// </summary>
        public string ExpandInfoID { get; init; } = new("");

        /// <summary>
        /// Styles can be independent, although styles marked for unique elements not taken into consideration if reused - or checkmark - (apply only for any page?)
        /// Consider css - how much can you reuse idea? Maybe generate css dynamically? Or use from created earlier dynamically..
        /// Has to be global and unique version, so user does not change for others accidentally.
        /// </summary>
        public string StyleID { get; init; } = new("");

        /// <summary>
        /// Localized, sorting & filtering, styling, hidden items, hidden item types (links.. pseudo-properties), alternative content version,
        /// Suggested new versions..
        /// Allow to create versions groups... have some predefined ones (above) - change history in it as well, even multiple ones?
        /// </summary>
        public List<string> VersionsIDs { get; init; } = new();

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

        public bool IsSubPage(string id) => true;
            //SubPages.FirstOrDefault(p => p.ID == id) != null;
    }
}
