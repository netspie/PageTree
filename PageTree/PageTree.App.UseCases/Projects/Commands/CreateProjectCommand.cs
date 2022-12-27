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
    private IRepository<User> _userRepository;
    private IRepository<Project> _projectRepository;
    private IRepository<ProjectUserList> _projectUserListRepository;
    private IRepository<Page> _pageRepository;

    public CreateProjectCommandHandler(
         IRepository<User> userRepository,
         IRepository<Project> projectRepository,
         IRepository<ProjectUserList> projectUserListRepository,
         IRepository<Page> pageRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _projectUserListRepository = projectUserListRepository;
        _pageRepository = pageRepository;
    }

    public async ValueTask<Result> Handle(CreateProjectCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var (user, projectList) = await _userRepository.GetOwnerAndChildOrCreateChild(_projectUserListRepository, command.UserID, nameof(User.ProjectUserListID), result);

        var rootPage = new Page(NewID);

        var projectID = NewID;
        if (!projectList.ProjectsCreatedIDs.Create(projectID))
            return result.Fail();

        var project = new Project(projectID, rootPage.ID);

        await _pageRepository.Save(rootPage, result);
        await _projectRepository.Save(project, result);
        await _projectUserListRepository.Save(projectList, result);

        return result;
    }
}

public sealed record CreateProjectCommand(string UserID) : ICommand<Result>;
