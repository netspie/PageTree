using Common.Basic.DDD;

namespace PageTree.App.Entities.Signatures
{
    public class Signature : Entity
    {
        public string Name { get; init; } = new("");
        public string ParentID { get; init; } = new("");
        public List<string> ChildrenIDs { get; init; } = new();
        public List<string> StyleIDs { get; init; } = new();
    }
}
