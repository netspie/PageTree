using PageTree.App.UseCases.PracticeTactics.Commands;
using PageTree.App.UseCases.PracticeTactics.Queries;

namespace PageTree.App.UseCases.PracticeTactics.Common
{
    public static class PracticeTacticItemVMExtensions
    {
        public static PracticeTacticItemVM ToCommandVM(this PracticeTacticVM vm) =>
            new()
            {
                PageItems = vm.PageItems.Select(i => new PageItemIDsVM()
                {
                    PageSignatures = i.PageSignatures.Select(s => s.ID).ToArray(),
                    QuestionsSignatures = i.QuestionsSignatures.Select(s => s.ID).ToArray(),
                    AnswersSignatures = i.AnswersSignatures.Select(s => s.ID).ToArray(),

                }).ToArray(),

                PagesToSkipIfContainsID = vm.PagesToSkipIfContainsID.Select(s => s.ID).ToArray(),
                PagesToSkipIfNotContainsID = vm.PagesToSkipIfNotContainsID.Select(s => s.ID).ToArray(),
            };
    }
}
