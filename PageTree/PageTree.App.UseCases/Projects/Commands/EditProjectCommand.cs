using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.CQRS.Command.Types;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Projects;

namespace PageTree.App.Projects.Commands;

public class EditProjectCommandHandler : BaseCommandHandler, ICommandHandler<EditProjectCommand, Result>
{
    private IRepository<Project> _projectRepository;

    public EditProjectCommandHandler(
         IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async ValueTask<Result> Handle(EditProjectCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var project = await _projectRepository.Get(command.ID, result);
        if (project == null || !result.ValidateSuccessAndValues())
            return Result.Failure();

        project.Name = command.Name;
        project.Description = command.Description;

        await _projectRepository.Save(project, result);

        return result;
    }
}

public sealed record EditProjectCommand(string ID, string Name, string Description) : ICommand<Result>, IReplaceCommand;
