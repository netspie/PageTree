using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.App.UseCases.Users.Commands
{
    public class CreateUserCommandHandler : BaseCommandHandler, ICommandHandler<CreateUserCommand, Result>
    {
        private IRepository<User> _userRepository;
        private IRepository<ProjectUserList> _projectUserListRepository;

        public CreateUserCommandHandler(
            IRepository<User> userRepository, 
            IRepository<ProjectUserList> projectUserListRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
        }

        public async ValueTask<Result> Handle(CreateUserCommand command, CancellationToken ct)
        {
            var result = Result.Success();

            var user = await _userRepository.Get(command.UserID, result);
            if (!result.IsSuccess || user != null)
                return result;

            user = new User(command.UserID);

            var projectUserList = new ProjectUserList(NewID, user.ID);
            await _projectUserListRepository.Save(projectUserList, result);
            if (!result.ValidateSuccessAndValues())
                return result;

            user.ProjectUserListID = projectUserList.ID;

            await _userRepository.Save(user, result);

            return result;
        }
    }

    public record CreateUserCommand(string UserID) : ICommand<Result>;
}
