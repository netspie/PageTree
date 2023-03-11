using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.PageTemplates;

namespace PageTree.App.PageTemplates.Commands;

public class RemovePropertyTemplateCommandHandler : BaseCommandHandler, ICommandHandler<RemovePropertyTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public RemovePropertyTemplateCommandHandler(
        IRepository<PageTemplate> _pageRepository)
    {
        _pageTemplateRepository = _pageRepository;
    }

    public async ValueTask<Result> Handle(RemovePropertyTemplateCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parentPage = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.IsSuccess || parentPage == null)
            return result.Fail();

        var deletedPages = new List<IdentityVM>();
        result += await DeleteThisAndNestedSubPages(_pageTemplateRepository, parentPage, command.PropertyTemplateID, deletedPages);
        if (!result.IsSuccess)
            return result;

        await _pageTemplateRepository.Save(parentPage, result);

        return result;
    }

    private static async Task<Result> DeleteThisAndNestedSubPages(IRepository<PageTemplate> repository, PageTemplate parentPage, string propertyID, List<IdentityVM> deletedPages)
    {
        var res = Result.Success();

        bool isSubPage = parentPage.IsSubPage(propertyID);
        parentPage.RemoveProperty(propertyID);
        if (!isSubPage)
            return res;

        var propertyPage = await repository.Get(propertyID, res);

        var subPages = propertyPage.SubPages.ToArray();
        foreach (var propertyPageSubPageID in subPages)
            res += await DeleteThisAndNestedSubPages(repository, propertyPage, propertyPageSubPageID, deletedPages);

        var deleteResult = await repository.Delete(propertyID);
        if (!deleteResult.IsSuccess)
            return res.Fail();

        deletedPages.Add(new(propertyPage.ID, propertyPage.Name));
        res += deleteResult;

        return res;
    }
}

public sealed record RemovePropertyTemplateCommand(string PageTemplateID, string PropertyTemplateID) : ICommand<Result>;
