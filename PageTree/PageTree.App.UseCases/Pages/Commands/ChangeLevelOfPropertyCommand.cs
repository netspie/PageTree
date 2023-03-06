using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class ChangeLevelOfPropertyCommandHandler : BaseCommandHandler, ICommandHandler<ChangeLevelOfPropertyCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public ChangeLevelOfPropertyCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(ChangeLevelOfPropertyCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var currentParentPage = await _pageRepository.Get(command.PageID, result);
        var propertyPage = await _pageRepository.Get(command.PropertyID, result);
        var newParentPage = await _pageRepository.Get(command.NewPageID, result);

        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        if (!newParentPage.MoveProperty(currentParentPage, propertyPage, out var isSubPage))
            return result.Fail();

        await _pageRepository.Save(currentParentPage, result);
        await _pageRepository.Save(newParentPage, result);
        if (isSubPage)
            await _pageRepository.Save(propertyPage, result);

        return result;
    }
}

public sealed record ChangeLevelOfPropertyCommand(string PageID, string PropertyID, string NewPageID) : ICommand<Result>;
