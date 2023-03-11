using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class ChangeLevelOfPropertyTemplateCommandHandler : BaseCommandHandler, ICommandHandler<ChangeLevelOfPropertyTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangeLevelOfPropertyTemplateCommandHandler(
         IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangeLevelOfPropertyTemplateCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var currentParentPage = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        var propertyPage = await _pageTemplateRepository.Get(command.PropertyTemplateID, result);
        var newParentPage = await _pageTemplateRepository.Get(command.NewPageTemplateID, result);

        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        if (!newParentPage.MoveProperty(currentParentPage, propertyPage, out var isSubPage))
            return result.Fail();

        await _pageTemplateRepository.Save(currentParentPage, result);
        await _pageTemplateRepository.Save(newParentPage, result);
        if (isSubPage)
            await _pageTemplateRepository.Save(propertyPage, result);

        return result;
    }
}

public sealed record ChangeLevelOfPropertyTemplateCommand(string PageTemplateID, string PropertyTemplateID, string NewPageTemplateID) : ICommand<Result>;
