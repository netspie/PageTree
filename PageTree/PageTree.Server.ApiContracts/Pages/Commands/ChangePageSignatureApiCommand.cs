namespace PageTree.Server.ApiContracts
{
    public class ChangePageSignatureApiCommand
    {
        public string PageID { get; set; }
        public string SignatureID { get; set; }
    }
}
