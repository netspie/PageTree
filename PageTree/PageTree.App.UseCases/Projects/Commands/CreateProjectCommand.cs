using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;
using Practicer.Domain.Pages.Common;

namespace PageTree.App.Projects.Commands;

public class CreateProjectCommandHandler : BaseCommandHandler, ICommandHandler<CreateProjectCommand, Result>
{
    private IRepository<Project> _projectRepository;
    private IRepository<ProjectUserList> _projectUserListRepository;
    private IRepository<Page> _pageRepository;

    public CreateProjectCommandHandler(
         IRepository<Project> projectRepository,
         IRepository<ProjectUserList> projectUserListRepository,
         IRepository<Page> pageRepository)
    {
        _projectRepository = projectRepository;
        _projectUserListRepository = projectUserListRepository;
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(CreateProjectCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var projectList = await _projectUserListRepository.Get(command.ProjectUserListID, result);
        if (!result.IsSuccess || projectList == null)
            return result.Fail();

        var rootPage = new Page(NewID);

        var project = new Project(NewID, rootPage.ID, projectList.OwnerID);
        if (!projectList.ProjectsCreatedIDs.Create(project.ID))
            return result.Fail();

        await _pageRepository.Save(rootPage, result);
        await _projectRepository.Save(project, result);
        await _projectUserListRepository.Save(projectList, result);

        return result;
    }
}

public sealed record CreateProjectCommand(string ProjectUserListID) : ICommand<Result>;
