using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class ChangeNameOfPageTemplatePageCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfPageTemplatePageCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangeNameOfPageTemplatePageCommandHandler(
        IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangeNameOfPageTemplatePageCommand command, CancellationToken ct)
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

public sealed record ChangeNameOfPageTemplatePageCommand(string PageTemplateID, string Name) : ICommand<Result>;
