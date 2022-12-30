using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.Domain.Projects;

namespace PageTree.App.Projects.Queries;

public class GetProjectOfIDQueryHandler : IQueryHandler<GetProjectOfIDQuery, Result<GetProjectOfIDQueryOut>>
{
    private IRepository<Project> _projectRepository;

    public GetProjectOfIDQueryHandler(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async ValueTask<Result<GetProjectOfIDQueryOut>> Handle(GetProjectOfIDQuery query, CancellationToken ct)
    {
        var result = Result<GetProjectOfIDQueryOut>.Success();

        var project = await _projectRepository.Get(query.ID, result);

        var @out = new GetProjectOfIDQueryOut(
            new ProjectVM(project.ID, project.Name, project.Description));

        return result.With(@out);
    }
}

public sealed record GetProjectOfIDQuery(string ID) : IQuery<Result<GetProjectOfIDQueryOut>>;
public sealed record GetProjectOfIDQueryOut(ProjectVM ProjectVM);

public sealed record ProjectVM(string ID, string Name, string Description);
