using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeCategories.Commands;

public class ChangeNameOfPracticeCategoryCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfPracticeCategoryCommand, Result>
{
    private readonly IRepository<PracticeCategory> _repository;

    public ChangeNameOfPracticeCategoryCommandHandler(
         IRepository<PracticeCategory> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(ChangeNameOfPracticeCategoryCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var item = await _repository.Get(command.PracticeCategoryID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var parent = await _repository.Get(item.ParentID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var siblings = await _repository.Get(parent.ChildrenIDs, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        if (siblings.Any(s => s.Name == command.Name))
            return result.Fail();

        if (!item.Rename(command.Name))
            return result.Fail();

        await _repository.Save(item, result);

        return result;
    }
}

public sealed record ChangeNameOfPracticeCategoryCommand(string PracticeCategoryID, string Name) : ICommand<Result>;
