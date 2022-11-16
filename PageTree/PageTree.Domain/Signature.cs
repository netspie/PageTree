using Common.Basic.DDD;

namespace PageTree.Domain
{
    public class Signature : Entity
    {
        public string Name { get; init; } = string.Empty;

        public string ParentID { get; init; } = string.Empty;
        public List<string> ChildrenIDs { get; init; } = new List<string>();

    }
}
