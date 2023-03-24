using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeTactics.Commands;

public class UpdateDataOfPracticeTacticCommandHandler : BaseCommandHandler, ICommandHandler<UpdateDataOfPracticeTacticCommand, Result>
{
    private readonly IRepository<PracticeTactic> _repository;

    public UpdateDataOfPracticeTacticCommandHandler(
         IRepository<PracticeTactic> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(UpdateDataOfPracticeTacticCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var item = await _repository.Get(command.PracticeTacticID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var data = command.Data;
        item.PageItems = data.PageItems.SelectOrDefault(i => new PracticeTacticItem()
        {
            PageSignaturesIDs = i.PageSignatures.SelectOrDefault(id => id).ToList(),
            QuestionsSignatureIDs = i.QuestionsSignatures.SelectOrDefault(id => id).ToList(),
            AnswersSignatureIDs = i.AnswersSignatures.SelectOrDefault(id => id).ToList(),
        }).ToList();

        item.SkipItemIfContainsOfIDs = data.PagesToSkipIfContainsID.SelectOrDefault(id => id).ToList();
        item.SkipItemIfNotContainsOfIDs = data.PagesToSkipIfNotContainsID.SelectOrDefault(id => id).ToList();

        await _repository.Save(item, result);

        return result;
    }
}

public sealed record UpdateDataOfPracticeTacticCommand(string PracticeTacticID, PracticeTacticItemVM Data) : ICommand<Result>;

public class PracticeTacticItemVM
{
    public PageItemIDsVM[] PageItems { get; set; }

    public string[] PagesToSkipIfContainsID { get; set; }
    public string[] PagesToSkipIfNotContainsID { get; set; }
}

public class PageItemIDsVM
{
    public string[] PageSignatures { get; set; }
    public string[] QuestionsSignatures { get; set; }
    public string[] AnswersSignatures { get; set; }
}
