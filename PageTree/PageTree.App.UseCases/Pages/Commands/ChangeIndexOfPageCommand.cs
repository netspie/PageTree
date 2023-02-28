using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class ChangeIndexOfPageCommandHandler : BaseCommandHandler, ICommandHandler<ChangeIndexOfPageCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public ChangeIndexOfPageCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(ChangeIndexOfPageCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();

        if (!page.ReorderProperty(command.PropertyID, command.Index))
            return result.Fail();

        await _pageRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangeIndexOfPageCommand(string PageID, string PropertyID, int Index) : ICommand<Result>;
