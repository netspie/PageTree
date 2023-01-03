using Common.Basic.DDD;

namespace PageTree.Domain.Practice
{
    public class PracticeTactic : Entity
    {
        public PracticeTactic() { }
        public PracticeTactic(string id) : base(id) { }

        public string Name { get; init; } = new("");
        public string PracticeCategoryID { get; init; } = new("");
        public List<PracticeTacticItem> PageItems { get; init; } = new();
        public List<string> Items { get; init; } = new();
        public List<string> SkipItemIfContainsOfIDs { get; init; } = new();
        public List<string> SkipItemIfNotContainsOfIDs { get; init; } = new();
    }
}
