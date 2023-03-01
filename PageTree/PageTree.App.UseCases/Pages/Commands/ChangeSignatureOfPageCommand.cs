using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class ChangeSignatureOfPageCommandHandler : BaseCommandHandler, ICommandHandler<ChangeSignatureOfPageCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public ChangeSignatureOfPageCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(ChangeSignatureOfPageCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();

        if (!page.Resignature(command.SignatureID))
            return result.Fail();

        await _pageRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangeSignatureOfPageCommand(string PageID, string SignatureID) : ICommand<Result>;
