using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Projects;

namespace PageTree.App.Projects.Commands;

public class ArchiveProjectCommandHandler : BaseCommandHandler, ICommandHandler<ArchiveProjectCommand, Result>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<ProjectUserList> _projectUserListRepository;

    public ArchiveProjectCommandHandler(
         IRepository<Project> projectRepository,
         IRepository<ProjectUserList> projectUserListRepository)
    {
        _projectRepository = projectRepository;
        _projectUserListRepository = projectUserListRepository;
    }

    public async ValueTask<Result> Handle(ArchiveProjectCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var projectList = await _projectUserListRepository.Get(command.ProjectListID, result);
        if (!result.IsSuccess || projectList == null)
            return result.Fail();

        var project = await _projectRepository.Get(command.ID, result);
        if (!result.IsSuccess || project == null)
            return result.Fail();

        if (!projectList.ProjectsCreatedIDs.Remove(project.ID))
            return result.Fail();

        projectList.ProjectsArchivedIDs.Add(project.ID);

        await _projectRepository.Save(project, result);
        await _projectUserListRepository.Save(projectList, result);

        return result;
    }
}

public sealed record ArchiveProjectCommand(string ID, string ProjectListID) : ICommand<Result>;
