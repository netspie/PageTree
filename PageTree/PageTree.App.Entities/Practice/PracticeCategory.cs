using Common.Basic.DDD;

namespace PageTree.Domain.Practice
{
    public class PracticeCategory : Entity
    {
        public PracticeCategory() {}
        public PracticeCategory(string id) : base(id) {}

        public string Name { get; init; } = new("");
        public List<string> Items { get; init; } = new();
    }
}
