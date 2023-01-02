using Common.Basic.Blocks;
using Common.Basic.Repository;
using Corelibs.Basic.Architecture.CQRS.Query.Types;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Projects;

namespace PageTree.App.Projects.Queries;

public class GetProjectQueryHandler : Mediator.IQueryHandler<GetProjectQuery, Result<GetProjectQueryOut>>
{
    private IRepository<Project> _projectRepository;

    public GetProjectQueryHandler(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async ValueTask<Result<GetProjectQueryOut>> Handle(GetProjectQuery query, CancellationToken ct)
    {
        var result = Result<GetProjectQueryOut>.Success();

        var project = await _projectRepository.Get(query.ID, result);

        var @out = new GetProjectQueryOut(
            new ProjectVM(project.ID, project.Name, project.Description));

        return result.With(@out);
    }
}

public sealed record GetProjectQuery(string ID) : IQuery<Result<GetProjectQueryOut>>, IGetQuery;
public sealed record GetProjectQueryOut(ProjectVM ProjectVM);

public sealed record ProjectVM(string ID, string Name, string Description) : QueryOut;
