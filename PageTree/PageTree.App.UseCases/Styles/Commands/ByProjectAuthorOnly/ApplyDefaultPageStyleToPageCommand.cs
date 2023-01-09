namespace PageTree.App.UseCases.Styles
{
    /// <summary>
    /// Applies a page style for a single page. Overrides a style applied for a page signature if such exists.
    /// </summary>
    public class ApplyDefaultPageStyleToPageCommand
    {
        /// <summary>
        /// An id the page style.
        /// </summary>
        public string PageStyleID { get; init; }

        /// <summary>
        /// An id the page.
        /// </summary>
        public string PageID { get; init; }
    }
}
