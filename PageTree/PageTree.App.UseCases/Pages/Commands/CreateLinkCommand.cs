using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class CreateLinkCommandHandler : BaseCommandHandler, ICommandHandler<CreateLinkCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public CreateLinkCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(CreateLinkCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.IsSuccess || page == null)
            return result.Fail();

        if (!page.CreateLink(command.LinkID, command.Index))
            return result.Fail();

        await _pageRepository.Save(page, result);

        return result;
    }
}

public sealed record CreateLinkCommand(string PageID, string LinkID, int Index = int.MaxValue) : ICommand<Result>;
