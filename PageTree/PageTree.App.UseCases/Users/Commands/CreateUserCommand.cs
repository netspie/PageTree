using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.DDD;
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
        private IAccessor<CurrentUser> _currentUserAccessor;

        public CreateUserCommandHandler(
            IRepository<User> userRepository,
            IRepository<ProjectUserList> projectUserListRepository,
            IAccessor<CurrentUser> currentUserAccessor)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _currentUserAccessor = currentUserAccessor;
        }

        public async ValueTask<Result> Handle(CreateUserCommand command, CancellationToken ct)
        {
            var result = Result.Success();

            var currentUser = _currentUserAccessor.Get();

            var user = await _userRepository.Get(currentUser.ID, result);
            if (!result.IsSuccess || user != null)
                return result;

            user = new User(currentUser.ID);

            var projectUserList = new ProjectUserList(NewID, user.ID);
            await _projectUserListRepository.Save(projectUserList, result);
            if (!result.ValidateSuccessAndValues())
                return result;

            user.ProjectUserListID = projectUserList.ID;

            await _userRepository.Save(user, result);

            return result;
        }
    }

    public record CreateUserCommand() : ICommand<Result>;
}
