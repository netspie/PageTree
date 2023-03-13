using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class CreateSubPageTemplateCommandHandler : BaseCommandHandler, ICommandHandler<CreateSubPageTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public CreateSubPageTemplateCommandHandler(
        IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(CreateSubPageTemplateCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parentPage = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.IsSuccess || parentPage == null)
            return result.Fail();

        var subPage = new PageTemplate(NewID, "New Template", "New Template Page", parentPage.OwnerID, parentPage.ProjectID)
        {
            ParentID = parentPage.ID,
        };
        if (!parentPage.CreateSubPage(subPage.ID, command.Index))
            return result.Fail();

        await _pageTemplateRepository.Save(subPage, result);
        await _pageTemplateRepository.Save(parentPage, result);

        return result;
    }
}

public sealed record CreateSubPageTemplateCommand(string PageTemplateID, int Index = int.MaxValue) : ICommand<Result>;
