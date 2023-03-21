using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeCategories.Commands;

public class DeletePracticeCategoryCommandHandler : BaseCommandHandler, ICommandHandler<DeletePracticeCategoryCommand, Result>
{
    private readonly IRepository<PracticeCategory> _repository;

    public DeletePracticeCategoryCommandHandler(
         IRepository<PracticeCategory> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(DeletePracticeCategoryCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var item = await _repository.Get(command.PracticeCategoryID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var parent = await _repository.Get(item.ParentID, result);
        if (!parent.RemovePracticeCategory(item.ID))
            return result.Fail();

        await _repository.Delete(item.ID);
        await _repository.Save(parent, result);

        return result;
    }
}

public sealed record DeletePracticeCategoryCommand(string PracticeCategoryID) : ICommand<Result>;
