using Common.Basic.DDD;

namespace PageTree.App.Entities.PageVersions
{
    public class PageVersion : Entity
    {
        public string PageID { get; init; } = string.Empty;
        public string Name { get; set; } = null;

        public string ExpandInfoID { get; set; } = string.Empty;
        public string StyleID { get; set; } = null;
    }

    public class ChildrenConfiguration
    {
        public List<ChildrenModification> Modifications { get; init; }
    }

    public class ChildrenModification
    {
        public string ID { get; init; } = string.Empty;

        public ChildrenModificationType ModificationType { get; init; }
        public Ordering Ordering { get; set; }
        public Sorting Sorting { get; set; }
        public Filtering Filtering { get; set; }
        public Pagination Pagination { get; set; }
    }

    public enum ChildrenModificationType
    {
        Ordering, 
        Sorting,
        Filtering,
        Pagination
    }

    public class Ordering
    {
        public List<OrderItem> Items { get; init; } = new();
    }

    public class OrderItem
    {
        public string ID { get; init; }
        public int Index { get; init; }
    }

    public class Sorting
    {
        public SortingType SortingType { get; init; }
    }

    public enum Artifact
    {
        Name,
        Signature,
        PropertyType
    }

    public enum PropertyType
    {
        SubPage,
        Link,
        CompoundLink, // ?? Query Link ?
        Query,

        PseudoProperty
    }

    public enum SortingType
    {
        Alphabetical,
        Random
    }

    public enum SortingOrder
    {
        Ascending,
        Descending
    }

    public class Filtering
    {

    }

    public class Pagination
    {

    }
}
