using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Searching;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class ChangeNameOfPageCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfPageCommand, Result>
{
    private readonly ISearchEngine<Page> _searchEngine;
    private readonly IRepository<Page> _pageRepository;

    public ChangeNameOfPageCommandHandler(
        ISearchEngine<Page> searchEngine,
        IRepository<Page> pageRepository)
    {
        _searchEngine = searchEngine;
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(ChangeNameOfPageCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();
        
        if (!page.Rename(command.Name))
            return result.Fail();

        await _pageRepository.Save(page, result);

        if (result.IsSuccess)
            _searchEngine.Update(new(page.ID, page.Name), command.Name);

        return result;
    }
}

public sealed record ChangeNameOfPageCommand(string PageID, string Name) : ICommand<Result>;
