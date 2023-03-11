using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class ChangeNameOfPageTemplateCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfPageTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangeNameOfPageTemplateCommandHandler(
        IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangeNameOfPageTemplateCommand command, CancellationToken ct)
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

public sealed record ChangeNameOfPageTemplateCommand(string PageTemplateID, string Name) : ICommand<Result>;
