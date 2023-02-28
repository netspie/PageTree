using Common.Basic.Blocks;
using Common.Basic.Functional;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Pages.Commands;

public class UpdatePageCommandHandler : BaseCommandHandler, ICommandHandler<UpdatePageCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;

    public UpdatePageCommandHandler(
         IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(UpdatePageCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        // Get
        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.IsSuccess || page == null)
            return result.Fail();
        
        // Modify Name
        if (command.Name != page.Name)
            if (!page.Rename(command.Name))
                return result.Fail();

        // Modify Parent ID
        if (!command.ParentPageID.IsNull() && command.ParentPageID != page.ParentID)
            ;

        await _pageRepository.Save(page, result);

        return result;
    }
}

public sealed record UpdatePageCommand(
    string PageID, 
    string ParentPageID = null, 
    string Name = null) : ICommand<Result>;
