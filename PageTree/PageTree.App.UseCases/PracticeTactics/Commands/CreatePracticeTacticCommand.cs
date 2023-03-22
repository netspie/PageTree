using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeCategories.Commands;

public class CreatePracticeTacticCommandHandler : BaseCommandHandler, ICommandHandler<CreatePracticeTacticCommand, Result>
{
    private readonly IRepository<PracticeCategory> _repository;

    public CreatePracticeTacticCommandHandler(
         IRepository<PracticeCategory> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(CreatePracticeTacticCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parent = await _repository.Get(command.ParentID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var entity = new PracticeCategory(NewID, $"Practice Category - {new string(NewID.Take(8).ToArray())}", parent.OwnerID, parent.ProjectID, command.ParentID);
        if (!parent.CreatePracticeCategory(entity.ID, command.Index))
            return result.Fail();

        await _repository.Save(entity, result);
        await _repository.Save(parent, result);

        return result;
    }
}

public sealed record CreatePracticeTacticCommand(string ParentID, int Index) : ICommand<Result>;
