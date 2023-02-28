using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PageTree.App.Projects.Commands;

public class RemovePropertyCommandHandler : BaseCommandHandler, ICommandHandler<RemovePropertyCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public RemovePropertyCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(RemovePropertyCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parentPage = await _pageRepository.Get(command.PageID, result);
        if (!result.IsSuccess || parentPage == null)
            return result.Fail();

        await DeleteThisAndNestedSubPages(_pageRepository, parentPage, command.PropertyID);
        await _pageRepository.Save(parentPage, result);

        return result;
    }

    private static async Task DeleteThisAndNestedSubPages(IRepository<Page> repository, Page parentPage, string propertyID)
    {
        if (!parentPage.IsSubPage(propertyID))
            return;

        var res = Result.Success();
        var propertyPage = await repository.Get(propertyID, res);

        var subPages = propertyPage.SubPages.ToArray();
        foreach (var propertyPageSubPageID in subPages)
            await DeleteThisAndNestedSubPages(repository, propertyPage, propertyPageSubPageID);

        if (parentPage.IsSubPage(propertyID))
        {
            parentPage.RemoveProperty(propertyID);
            await repository.Delete(propertyID);
        }
    }
}

public sealed record RemovePropertyCommand(string PageID, string PropertyID) : ICommand<Result>;
