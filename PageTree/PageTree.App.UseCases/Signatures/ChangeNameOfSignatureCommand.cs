namespace PageTree.App.UseCases.Signatures
{
    public class ChangeNameOfSignatureCommand
    {
        public string SignatureID { get; init; }
        public string Name { get; init; }
    }
}
