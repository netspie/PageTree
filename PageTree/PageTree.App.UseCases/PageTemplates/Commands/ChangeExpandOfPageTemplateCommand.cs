using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class ChangeExpandOfPageTemplateCommandHandler : BaseCommandHandler, ICommandHandler<ChangeExpandOfPageTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangeExpandOfPageTemplateCommandHandler(
         IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangeExpandOfPageTemplateCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();

        if (page.IsExpanded == command.IsExpanded)
            return result.Fail();

        page.IsExpanded = command.IsExpanded;
        await _pageTemplateRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangeExpandOfPageTemplateCommand(string PageTemplateID, bool IsExpanded) : ICommand<Result>;
