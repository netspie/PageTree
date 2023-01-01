namespace PageTree.Domain.Practice
{
    public class PracticeTacticItem
    {
        public List<string> PageSignaturesParentsIDs { get; init; } = new List<string>();
        public List<string> PageSignaturesIDs { get; init; } = new List<string>();
        public List<string> QuestionsSignatureIDs { get; init; } = new List<string>();
        public List<string> AnswersSignatureIDs { get; init; } = new List<string>();
        public List<string> Items { get; init; } = new List<string>();
    }
}
