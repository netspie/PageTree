using Common.Basic.DDD;

namespace PageTree.Domain.Practice
{
    public class PracticeCategory : Entity
    {
        public string Name { get; init; } = string.Empty;
        public List<string> Items { get; init; } = new List<string>();
    }
}
