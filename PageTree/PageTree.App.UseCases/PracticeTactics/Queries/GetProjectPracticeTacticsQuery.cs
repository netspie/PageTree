using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;

namespace PageTree.App.UseCases.PracticeCategories.Queries;

public class GetProjectPracticeTacticsQueryHandler : IQueryHandler<GetProjectPracticeTacticsQuery, Result<GetProjectPracticeTacticsQueryOut>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<PracticeCategory> _practiceCategoryRepository;

    public GetProjectPracticeTacticsQueryHandler(
        IRepository<Project> projectRepository,
        IRepository<PracticeCategory> practiceCategoryRepository)
    {
        _projectRepository = projectRepository;
        _practiceCategoryRepository = practiceCategoryRepository;
    }

    public async ValueTask<Result<GetProjectPracticeTacticsQueryOut>> Handle(GetProjectPracticeTacticsQuery query, CancellationToken ct)
    {
        var result = Result<GetProjectPracticeTacticsQueryOut>.Success();

        var project = await _projectRepository.Get(query.ProjectID, result);
        var practiceCategoryRoot = await _practiceCategoryRepository.Get(project.PracticeCategoryRootID, result);

        var @out = new GetProjectPracticeTacticsQueryOut(
           new PracticeTacticsListVM() { PracticeCategoriesRootID = practiceCategoryRoot.ID });

        var practiceCategoriesIDs = practiceCategoryRoot.ChildrenIDs;
        if (practiceCategoriesIDs.IsNullOrEmpty())
            return result.With(@out);

        var practiceCategories = await _practiceCategoryRepository.Get(practiceCategoriesIDs, result);
        practiceCategories = practiceCategories.OrderBy(s => practiceCategoriesIDs.IndexOf(s.ID)).ToArray();

        @out = new GetProjectPracticeTacticsQueryOut(
            new PracticeTacticsListVM() 
            {
                PracticeCategoriesRootID = practiceCategoryRoot.ID,
                Values = practiceCategories.Select(s => new PracticeTacticVM()
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
    public string PracticeCategoriesRootID { get; set; } = "";
    public PracticeTacticVM[] Values { get; set; } = Array.Empty<PracticeTacticVM>();
}

public class PracticeTacticVM
{
    public IdentityVM Identity { get; set; } = new();
}
