using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeTactics.Commands;

public class ChangeIndexOfPracticeTacticCommandHandler : BaseCommandHandler, ICommandHandler<ChangeIndexOfPracticeTacticCommand, Result>
{
    private readonly IRepository<PracticeTactic> _repository;

    public ChangeIndexOfPracticeTacticCommandHandler(
         IRepository<PracticeTactic> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(ChangeIndexOfPracticeTacticCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var item = await _repository.Get(command.PracticeTacticID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var parent = await _repository.Get(item.ParentID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        if (!parent.MovePracticeTactic(command.PracticeTacticID, command.Index))
            return result.Fail();

        await _repository.Save(parent, result);

        return result;
    }
}

public sealed record ChangeIndexOfPracticeTacticCommand(string PracticeTacticID, int Index) : ICommand<Result>;
