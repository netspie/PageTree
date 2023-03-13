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

        var pageLinking = await _pageRepository.Get(command.PageID, result);
        var pageToLink = await _pageRepository.Get(command.LinkID, result);
        if (!result.ValidateSuccessAndValues() || pageLinking == null || pageToLink == null)
            return result.Fail();

        if (!pageLinking.CreateLink(command.LinkID, command.Index))
            return result.Fail();

        pageToLink.LinkedByIDs.Add(command.PageID);

        await _pageRepository.Save(pageLinking, result);
        await _pageRepository.Save(pageToLink, result);

        return result;
    }
}

public sealed record CreateLinkCommand(string PageID, string LinkID, int Index = int.MaxValue) : ICommand<Result>;
