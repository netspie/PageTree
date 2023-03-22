using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;

namespace PageTree.App.UseCases.PracticeTactics.Queries;

public class GetProjectPracticeTacticsQueryHandler : IQueryHandler<GetProjectPracticeTacticsQuery, Result<GetProjectPracticeTacticsQueryOut>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<PracticeTactic> _entityRepository;

    public GetProjectPracticeTacticsQueryHandler(
        IRepository<Project> projectRepository,
        IRepository<PracticeTactic> entityRepository)
    {
        _projectRepository = projectRepository;
        _entityRepository = entityRepository;
    }

    public async ValueTask<Result<GetProjectPracticeTacticsQueryOut>> Handle(GetProjectPracticeTacticsQuery query, CancellationToken ct)
    {
        var result = Result<GetProjectPracticeTacticsQueryOut>.Success();

        var project = await _projectRepository.Get(query.ProjectID, result);
        var entitiesRoot = await _entityRepository.Get(project.PracticeTacticRootID, result);

        var @out = new GetProjectPracticeTacticsQueryOut(
           new PracticeTacticsListVM() { RootID = entitiesRoot.ID });

        var childrenIDs = entitiesRoot.ChildrenIDs;
        if (childrenIDs.IsNullOrEmpty())
            return result.With(@out);

        var entities = await _entityRepository.Get(childrenIDs, result);
        entities = entities.OrderBy(s => childrenIDs.IndexOf(s.ID)).ToArray();

        @out = new GetProjectPracticeTacticsQueryOut(
            new PracticeTacticsListVM() 
            {
                RootID = entitiesRoot.ID,
                Values = entities.Select(s => new PracticeTacticVM()
                {
                    Identity = (s.ID, s.Name),
                })
                .ToArray()
            });

        return result.With(@out);
    }
}

public sealed record GetProjectPracticeTacticsQuery(string ProjectID) : IQuery<Result<GetProjectPracticeTacticsQueryOut>>;
public sealed record GetProjectPracticeTacticsQueryOut(PracticeTacticsListVM SignatureList);

public class PracticeTacticsListVM
{
    public string RootID { get; set; } = "";
    public PracticeTacticVM[] Values { get; set; } = Array.Empty<PracticeTacticVM>();
}

public class PracticeTacticVM
{
    public IdentityVM Identity { get; set; } = new();
}
