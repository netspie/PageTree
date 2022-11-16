namespace PageTree.Domain
{
    public class Style
    {
        public string ID { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;

        public List<Style> ChildrenStyles { get; init; } = new List<Style>();
    }
}
