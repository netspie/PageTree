namespace PageTree.App.UseCases.PageVersions
{
    public class UpdatePageVersionCommand
    {
        public string PageVersionID { get; init; }
        public string Name { get; set; }
    }
}
