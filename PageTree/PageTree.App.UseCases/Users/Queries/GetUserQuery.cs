using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.CQRS.Query;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.DDD;
using Mediator;
using PageTree.Domain.Users;

namespace PageTree.App.UseCases.Users.Queries;

public class GetUserQueryHandler : Mediator.IQueryHandler<GetUserQuery, Result<GetUserQueryOut>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IAccessor<CurrentUser> _currentUserAccessor;

    public GetUserQueryHandler(IRepository<User> userRepository, IAccessor<CurrentUser> currentUserAccessor)
    {
        _userRepository = userRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async ValueTask<Result<GetUserQueryOut>> Handle(GetUserQuery query, CancellationToken ct)
    {
        var result = Result<GetUserQueryOut>.Success();

        var userID = query.ID;
        if (userID.IsNullOrEmpty())
            userID = _currentUserAccessor.Get().ID;

        var user = await _userRepository.Get(userID, result);

        var @out = new GetUserQueryOut(user.ID, user.ProjectUserListID);

        return result.With(@out);
    }
}

public sealed record GetUserQuery(string ID) : IQuery<Result<GetUserQueryOut>>, IGetQuery
{
    public GetUserQuery() : this("") {}
}

public sealed record GetUserQueryOut(string ID, string ProjectUserListID);
