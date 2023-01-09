namespace PageTree.App.UseCases.Styles
{
    /// <summary>
    /// Applies a property style as default for all of properties of given page. Overrides a style applied for a page signature if such exists.
    /// </summary>
    public class ApplyPropertyStyleToPageCommand
    {
        public string PropertyStyleID { get; init; }

        /// <summary>
        /// An id of a parent page of the property.
        /// </summary>
        public string PageID { get; init; }
    }
}
