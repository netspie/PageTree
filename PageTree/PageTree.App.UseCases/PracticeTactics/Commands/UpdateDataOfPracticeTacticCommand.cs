using Common.Basic.Blocks;
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
        item.PageItems = data.PageItems.Select(i => new PracticeTacticItem()
        {
            PageSignaturesIDs = i.PageSignatures.Select(id => id).ToList(),
            QuestionsSignatureIDs = i.QuestionsSignatures.Select(id => id).ToList(),
            AnswersSignatureIDs = i.AnswersSignatures.Select(id => id).ToList(),
        }).ToList();

        item.SkipItemIfContainsOfIDs = data.PagesToSkipIfContainsID.Select(id => id).ToList();
        item.SkipItemIfNotContainsOfIDs = data.PagesToSkipIfNotContainsID.Select(id => id).ToList();

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
