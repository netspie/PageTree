namespace PageTree.App.Entities.Styles
{
    public class PageStyle
    {
        public string ID { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;

        public string DefaultPropertyStyleID { get; set; }

        // Name styles?

        public List<string> PropertyStyles { get; set; }

    }
}
