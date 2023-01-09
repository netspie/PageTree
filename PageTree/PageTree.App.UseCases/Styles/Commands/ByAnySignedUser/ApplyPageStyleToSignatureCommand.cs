namespace PageTree.App.UseCases.Styles
{
    /// <summary>
    /// Applies a page style for all pages of given signature.
    /// </summary>
    public class ApplyPageStyleToSignatureCommand
    {
        /// <summary>
        /// An id the page style.
        /// </summary>
        public string PageStyleID { get; init; }

        /// <summary>
        /// An id of the signature.
        /// </summary>
        public string SignatureID { get; init; }
    }
}
