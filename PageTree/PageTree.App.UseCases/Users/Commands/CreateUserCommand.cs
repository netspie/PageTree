using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.Domain.Users;

namespace PageTree.App.UseCases.Users.Commands
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
    {
        private IRepository<User> _userRepository;

        public CreateUserCommandHandler(IRepository<User> userRepository) =>
            _userRepository = userRepository;

        public async ValueTask<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken) =>
            await _userRepository.GetIfExistsOrCreateAndSave(command.UserID, () => new User(command.UserID));
    }

    public record CreateUserCommand(string UserID) : ICommand<Result>;
}
