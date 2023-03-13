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
        var pagesToSaveIfNotDeleted = new List<Page>();
        result += await DeleteThisAndNestedSubPages(_pageRepository, parentPage, command.PropertyID, deletedPages, pagesToSaveIfNotDeleted);
        if (!result.IsSuccess)
            return result;
        
        await _pageRepository.Save(parentPage, result);

        var pagesToSave = pagesToSaveIfNotDeleted
            .Where(p => deletedPages.FirstOrDefault(dp => dp.ID == p.ID) == null)
            .ToArray();

        result += await _pageRepository.Save(pagesToSave);

        if (isSubPage && result.IsSuccess)
        {
            foreach (var deletedPage in deletedPages)
                _searchEngine.Delete(new(deletedPage.ID, deletedPage.Name));
        }


        return result;
    }

    private static async Task<Result> DeleteThisAndNestedSubPages(IRepository<Page> repository, Page parentPage, string propertyID, 
        List<IdentityVM> deletedPages,
        List<Page> pagesToSaveIfNotDeleted)
    {
        var res = Result.Success();

        bool isSubPage = parentPage.IsSubPage(propertyID);
        parentPage.RemoveProperty(propertyID);

        var propertyPage = await repository.Get(propertyID, res);
        if (!isSubPage)
        {
            propertyPage.LinkedByIDs.Remove(parentPage.ID);
            return res;
        }

        propertyPage.LinkedByIDs.Remove(parentPage.ID);
        foreach (var linkedByID in propertyPage.LinkedByIDs)
        {
            var linkedByPage = await repository.Get(linkedByID, res);
            linkedByPage.RemoveProperty(propertyID);
            pagesToSaveIfNotDeleted.Add(linkedByPage);
        }

        var subPages = propertyPage.SubPages.ToArray();
        foreach (var propertyPageSubPageID in subPages)
            res += await DeleteThisAndNestedSubPages(repository, propertyPage, propertyPageSubPageID, deletedPages, pagesToSaveIfNotDeleted);

        var deleteResult = await repository.Delete(propertyID);
        if (!deleteResult.IsSuccess)
            return res.Fail();

        deletedPages.Add(new(propertyPage.ID, propertyPage.Name));
        res += deleteResult;

        return res;
    }
}

public sealed record RemovePropertyCommand(string PageID, string PropertyID) : ICommand<Result>;
