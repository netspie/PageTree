using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.PageTemplates;
using PageTree.App.UseCases.Common;

namespace PageTree.App.PageTemplates.Commands;

public class ChangePageTemplateNameCommandHandler : BaseCommandHandler, ICommandHandler<ChangePageTemplateNameCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangePageTemplateNameCommandHandler(
        IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangePageTemplateNameCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();
        
        if (!page.RenameTemplate(command.Name))
            return result.Fail();

        await _pageTemplateRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangePageTemplateNameCommand(string PageTemplateID, string Name) : ICommand<Result>;
