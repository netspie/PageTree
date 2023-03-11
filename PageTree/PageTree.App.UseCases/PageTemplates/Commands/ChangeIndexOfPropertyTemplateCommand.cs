using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class ChangeIndexOfPropertyTemplateCommandHandler : BaseCommandHandler, ICommandHandler<ChangeIndexOfPropertyTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangeIndexOfPropertyTemplateCommandHandler(
         IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangeIndexOfPropertyTemplateCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();

        if (!page.ReorderProperty(command.PropertyTemplateID, command.Index))
            return result.Fail();

        await _pageTemplateRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangeIndexOfPropertyTemplateCommand(string PageTemplateID, string PropertyTemplateID, int Index) : ICommand<Result>;
