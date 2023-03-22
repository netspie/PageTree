using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeTactics.Commands;

public class DeletePracticeTacticCommandHandler : BaseCommandHandler, ICommandHandler<DeletePracticeTacticCommand, Result>
{
    private readonly IRepository<PracticeTactic> _repository;

    public DeletePracticeTacticCommandHandler(
         IRepository<PracticeTactic> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(DeletePracticeTacticCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var item = await _repository.Get(command.PracticeTacticID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var parent = await _repository.Get(item.ParentID, result);
        if (!parent.RemovePracticeTactic(item.ID))
            return result.Fail();

        await _repository.Delete(item.ID);
        await _repository.Save(parent, result);

        return result;
    }
}

public sealed record DeletePracticeTacticCommand(string PracticeTacticID) : ICommand<Result>;
