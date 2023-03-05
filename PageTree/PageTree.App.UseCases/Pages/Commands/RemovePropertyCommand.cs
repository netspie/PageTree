using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Searching;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class RemovePropertyCommandHandler : BaseCommandHandler, ICommandHandler<RemovePropertyCommand, Result>
{
    private readonly ISearchEngine<Page> _searchEngine;
    private readonly IRepository<Page> _pageRepository;

    public RemovePropertyCommandHandler(
        ISearchEngine<Page> searchEngine,
        IRepository<Page> pageRepository)
    {
        _searchEngine = searchEngine;
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(RemovePropertyCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parentPage = await _pageRepository.Get(command.PageID, result);
        if (!result.IsSuccess || parentPage == null)
            return result.Fail();

        bool isSubPage = parentPage.IsSubPage(command.PropertyID);

        var deletedPages = new List<IdentityVM>();
        result += await DeleteThisAndNestedSubPages(_pageRepository, parentPage, command.PropertyID, deletedPages);
        if (!result.IsSuccess)
            return result;

        await _pageRepository.Save(parentPage, result);
        if (isSubPage && result.IsSuccess)
        {
            foreach (var deletedPage in deletedPages)
                _searchEngine.Delete(new(deletedPage.ID, deletedPage.Name));
        }

        return result;
    }

    private static async Task<Result> DeleteThisAndNestedSubPages(IRepository<Page> repository, Page parentPage, string propertyID, List<IdentityVM> deletedPages)
    {
        var res = Result.Success();

        bool isSubPage = parentPage.IsSubPage(propertyID);
        parentPage.RemoveProperty(propertyID);
        if (!isSubPage)
            return res;

        var propertyPage = await repository.Get(propertyID, res);

        var subPages = propertyPage.SubPages.ToArray();
        foreach (var propertyPageSubPageID in subPages)
            res += await DeleteThisAndNestedSubPages(repository, propertyPage, propertyPageSubPageID, deletedPages);

        var deleteResult = await repository.Delete(propertyID);
        if (!deleteResult.IsSuccess)
            return res.Fail();

        deletedPages.Add(new(propertyPage.ID, propertyPage.Name));
        res += deleteResult;

        return res;
    }
}

public sealed record RemovePropertyCommand(string PageID, string PropertyID) : ICommand<Result>;
