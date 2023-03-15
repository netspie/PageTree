using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Searching;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class CreateSubPageCommandHandler : BaseCommandHandler, ICommandHandler<CreateSubPageCommand, Result>
{
    private readonly ISearchEngine<Page> _searchEngine;
    private readonly IRepository<Page> _pageRepository;

    public CreateSubPageCommandHandler(
        ISearchEngine<Page> searchEngine,
        IRepository<Page> pageRepository)
    {
        _searchEngine = searchEngine;
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(CreateSubPageCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parentPage = await _pageRepository.Get(command.PageID, result);
        if (!result.IsSuccess || parentPage == null)
            return result.Fail();

        var subPage = new Page(NewID, "New Page", parentPage.OwnerID, parentPage.ProjectID)
        {
            ParentID = parentPage.ID,
        };
        if (!parentPage.CreateSubPage(subPage.ID, command.Index))
            return result.Fail();

        await _pageRepository.Save(subPage, result);
        await _pageRepository.Save(parentPage, result);

        if (!_searchEngine.Add(subPage.ID, subPage.Name))
            return result.Fail();

        return result;
    }
}

public sealed record CreateSubPageCommand(string PageID, int Index = int.MaxValue) : ICommand<Result>;
