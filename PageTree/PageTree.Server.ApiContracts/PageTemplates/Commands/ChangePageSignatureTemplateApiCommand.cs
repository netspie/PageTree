namespace PageTree.Server.ApiContracts
{
    public class ChangePageSignatureTemplateApiCommand
    {
        public string PageTemplateID { get; set; }
        public string SignatureID { get; set; }
    }
}
