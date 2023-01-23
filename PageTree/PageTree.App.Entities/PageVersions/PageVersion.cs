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
        Ordering, // if you wanna order pages manually by id
        Sorting, // if you wanna sort pages in a specific order by a condition
        Filtering, // if you wanna filter out results based on condition
        Pagination, // if you want to show only one group at a time but be able to navigate between groups
        Grouping, // if you want to group into virtual pages / folders, could be expanded or not, depending on amount or desire

        Include // if you want to first filter some results but also show others after that (some or all)
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
