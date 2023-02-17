namespace PageTree.App.UseCases.Signatures.Commands
{
    public class ChangeNameOfSignatureCommand
    {
        public string SignatureID { get; init; }
        public string Name { get; init; }
    }
}
