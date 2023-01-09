namespace PageTree.App.UseCases.Styles
{
    /// <summary>
    /// Applies a property style as default for all properties of given signature for all pages.
    /// </summary>
    public class ApplyPropertyStyleToSignatureCommand
    {
        /// <summary>
        /// An id of the signature.
        /// </summary>
        public string SignatureID { get; init; }
    }
}
