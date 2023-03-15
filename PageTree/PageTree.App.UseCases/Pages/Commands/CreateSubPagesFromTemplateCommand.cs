using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.Pages.Commands;

public class CreateSubPagesFromTemplateCommandHandler : BaseCommandHandler, ICommandHandler<CreateSubPagesFromTemplateCommand, Result>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<PageTemplate> _templateRepository;

    public CreateSubPagesFromTemplateCommandHandler(
        IRepository<Page> pageRepository,
        IRepository<PageTemplate> templateRepository)
    {
        _pageRepository = pageRepository;
        _templateRepository = templateRepository;
    }

    public async ValueTask<Result> Handle(CreateSubPagesFromTemplateCommand command, CancellationToken ct)
    {
        var result = new Result();

        var template = await _templateRepository.GetBy(command.PageTemplateID).AddAndGet(result);
        var page = await _pageRepository.GetBy(command.PageID).AddAndGet(result);
        if (!result.IsSuccess)
            return result;

        var pagesToSave = new List<Page>();
        var newPage = new Page(NewID, template.Name, template.SignatureID, page.ID);
        await Duplicate(template, newPage, pagesToSave);

        page.CreateSubPage(newPage.ID, command.Index);
        pagesToSave.Add(page);
        pagesToSave = pagesToSave.Distinct().ToList();

        foreach (var pageToSave in pagesToSave)
            await _pageRepository.Save(pageToSave);

        return Result.SuccessTask();
    }

    private async Task<Result> Duplicate(PageTemplate template, Page newPage, List<Page> pagesToSave)
    {
        pagesToSave.Add(newPage);
        foreach (var templateID in template.ChildrenIDs)
        {
            var result = new Result();
            var subTemplate = await _templateRepository.GetBy(templateID).AddAndGet(result);
            if (!result.IsSuccess)
                continue;

            var newSubPage = new Page(NewID, subTemplate.Name, subTemplate.SignatureID, newPage.ID);
            pagesToSave.Add(newSubPage);

            newPage.CreateSubPage(newSubPage.ID, int.MaxValue);
            await Duplicate(subTemplate, newSubPage, pagesToSave);
        }

        return Result.SuccessTask();
    }
}

public sealed record CreateSubPagesFromTemplateCommand(string PageID, string PageTemplateID, int Index = int.MaxValue) : ICommand<Result>;
