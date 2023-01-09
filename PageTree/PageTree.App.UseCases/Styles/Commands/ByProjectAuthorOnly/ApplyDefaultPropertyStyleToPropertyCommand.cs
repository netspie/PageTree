namespace PageTree.App.UseCases.Styles
{
    /// <summary>
    /// Applies a property style as default for a single property of given page. Overrides a style applied for a property signature if such exists.
    /// </summary>
    public class ApplyDefaultPropertyStyleToPropertyCommand
    {
        public string PropertyStyleID { get; init; }

        /// <summary>
        /// An id of a parent page of the property.
        /// </summary>
        public string PageID { get; init; }

        /// <summary>
        /// A unique identifier in scope of this page properties only. Not an id of a referenced page (link, subpage) or pseudo-property .
        /// </summary>
        public string PropertyID { get; init; }
    }
}
