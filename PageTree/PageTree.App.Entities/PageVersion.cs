namespace PageTree.Domain
{
    public class PageVersion
    {
        public string ID { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;

        public string PageID { get; init; } = string.Empty;

        public string ExpandInfoID { get; init; } = string.Empty;
        public string StyleID { get; init; } = string.Empty;
    }
}
