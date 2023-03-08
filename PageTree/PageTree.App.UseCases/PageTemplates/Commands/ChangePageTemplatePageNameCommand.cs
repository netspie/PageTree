using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.PageTemplates;
using PageTree.App.UseCases.Common;

namespace PageTree.App.PageTemplates.Commands;

public class ChangePageTemplatePageNameCommandHandler : BaseCommandHandler, ICommandHandler<ChangePageTemplatePageNameCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangePageTemplatePageNameCommandHandler(
        IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangePageTemplatePageNameCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();
        
        if (!page.Rename(command.Name))
            return result.Fail();

        await _pageTemplateRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangePageTemplatePageNameCommand(string PageTemplateID, string Name) : ICommand<Result>;
