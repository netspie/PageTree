using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Searching;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.Pages.Commands;

public class CreateSubPagesFromTemplateCommandHandler : BaseCommandHandler, ICommandHandler<CreateSubPagesFromTemplateCommand, Result>
{
    private readonly ISearchEngine<Page> _searchEngine;

    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<PageTemplate> _templateRepository;

    public CreateSubPagesFromTemplateCommandHandler(
        ISearchEngine<Page> searchEngine,
        IRepository<Page> pageRepository,
        IRepository<PageTemplate> templateRepository)
    {
        _searchEngine = searchEngine;
        _pageRepository = pageRepository;
        _templateRepository = templateRepository;
    }

    public async ValueTask<Result> Handle(CreateSubPagesFromTemplateCommand command, CancellationToken ct)
    {
        var result = new Result();

        var template = await _templateRepository.Get(command.PageTemplateID, result);
        var page = await _pageRepository.Get(command.PageID, result);
        if (!result.ValidateSuccessAndValues() || !template || !page)
            return result.Fail();

        var pagesToSave = new List<Page>();

        var newPage = new Page(NewID, template.Name, template.OwnerID, template.ProjectID)
        {
            SignatureID = template.SignatureID,
            ParentID = page.ID
        };

        result += await Duplicate(template, newPage, pagesToSave);
        if (!result.IsSuccess)
            return result;

        page.CreateSubPage(newPage.ID, command.Index);
        pagesToSave.Add(page);
        pagesToSave = pagesToSave.Distinct().ToList();

        foreach (var pageToSave in pagesToSave)
        {
            var saveResult = await _pageRepository.Save(pageToSave);
            if (!saveResult.IsSuccess)
                return result.Fail();

            result += saveResult;
        }

        var searchIndexData = pagesToSave.Select(p => new SearchIndexData(p.ID, p.Name));
        if (!_searchEngine.Add(searchIndexData))
            return result.Fail();

        return result;
    }

    private async Task<Result> Duplicate(PageTemplate template, Page newPage, List<Page> pagesToSave)
    {
        var result = new Result();

        pagesToSave.Add(newPage);
        foreach (var templateID in template.ChildrenIDs)
        {
            var subTemplate = await _templateRepository.Get(templateID, result);
            if (!result.IsSuccess)
                return result.Fail();

            var newSubPage = new Page(NewID, subTemplate.Name, subTemplate.OwnerID, subTemplate.ProjectID)
            {
                SignatureID = subTemplate.SignatureID,
                ParentID = newPage.ID
            };

            if (!newPage.CreateSubPage(newSubPage.ID, int.MaxValue))
                return result.Fail();

            result += await Duplicate(subTemplate, newSubPage, pagesToSave);
        }

        return result;
    }
}

public sealed record CreateSubPagesFromTemplateCommand(string PageID, string PageTemplateID, int Index = int.MaxValue) : ICommand<Result>;
