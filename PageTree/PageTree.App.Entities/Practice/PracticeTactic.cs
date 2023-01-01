using Common.Basic.DDD;

namespace PageTree.Domain.Practice
{
    public class PracticeTactic : Entity
    {
        public string Name { get; init; } = string.Empty;
        public string PracticeCategoryID { get; init; } = string.Empty;
        public List<PracticeTacticItem> PageItems { get; init; } = new List<PracticeTacticItem>();
        public List<string> Items { get; init; } = new List<string>();
        public List<string> SkipItemIfContainsOfIDs { get; init; } = new List<string>();
        public List<string> SkipItemIfNotContainsOfIDs { get; init; } = new List<string>();
    }
}
