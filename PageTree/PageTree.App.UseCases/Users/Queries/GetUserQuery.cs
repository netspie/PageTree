using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.Domain.Users;

namespace PageTree.App.UseCases.Users.Queries;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, Result<GetUserQueryOut>>
{
    private IRepository<User> _userRepository;

    public GetUserQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<Result<GetUserQueryOut>> Handle(GetUserQuery query, CancellationToken ct)
    {
        var result = Result<GetUserQueryOut>.Success();

        var user = await _userRepository.Get(query.ID, result);

        var @out = new GetUserQueryOut(user.ID, user.ProjectUserListID);

        return result.With(@out);
    }
}

public sealed record GetUserQuery(string ID) : IQuery<Result<GetUserQueryOut>>;
public sealed record GetUserQueryOut(string ID, string ProjectUserListID);
