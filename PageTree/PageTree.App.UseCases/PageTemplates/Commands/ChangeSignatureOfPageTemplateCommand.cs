using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.PageTemplates;
using PageTree.App.UseCases.Common;

namespace PageTree.App.PageTemplates.Commands;

public class ChangeSignatureOfPageTemplateCommandHandler : BaseCommandHandler, ICommandHandler<ChangeSignatureOfPageTemplateCommand, Result>
{
    private readonly IRepository<PageTemplate> _pageTemplateRepository;

    public ChangeSignatureOfPageTemplateCommandHandler(
         IRepository<PageTemplate> pageTemplateRepository)
    {
        _pageTemplateRepository = pageTemplateRepository;
    }

    public async ValueTask<Result> Handle(ChangeSignatureOfPageTemplateCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var page = await _pageTemplateRepository.Get(command.PageTemplateID, result);
        if (!result.ValidateSuccessAndValues() || page == null)
            return result.Fail();

        if (!page.Resignature(command.SignatureID))
            return result.Fail();

        await _pageTemplateRepository.Save(page, result);

        return result;
    }
}

public sealed record ChangeSignatureOfPageTemplateCommand(string PageTemplateID, string SignatureID) : ICommand<Result>;
