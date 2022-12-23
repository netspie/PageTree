namespace PageTree.Server.ApiContracts.Pages
{
    public class ChangePageSignatureApiCommand
    {
        public string PageID { get; set; }
        public string SignatureID { get; set; }
    }
}
