using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Projects.Commands;

public class ChangePageNameCommandHandler : BaseCommandHandler, ICommandHandler<ChangePageNameCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public ChangePageNameCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(ChangePageNameCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.IsSuccess || page == null)
            return result.Fail();
        
        if (!page.Rename(command.Name))
            return result.Fail();

        await _pageRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangePageNameCommand(string PageID, string Name) : ICommand<Result>;
