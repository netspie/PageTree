using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class ChangeNameOfPageCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfPageCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public ChangeNameOfPageCommandHandler(
         IRepository<Page> pageRepository)
    {
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

        return result;
    }
}

public sealed record ChangeNameOfPageCommand(string PageID, string Name) : ICommand<Result>;
