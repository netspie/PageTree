using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;

namespace PageTree.App.UseCases.PracticeCategories.Queries;

public class GetProjectPracticeCategoriesQueryHandler : IQueryHandler<GetProjectPracticeCategoriesQuery, Result<GetProjectPracticeCategoriesQueryOut>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<PracticeCategory> _practiceCategoryRepository;

    public GetProjectPracticeCategoriesQueryHandler(
        IRepository<Project> projectRepository,
        IRepository<PracticeCategory> practiceCategoryRepository)
    {
        _projectRepository = projectRepository;
        _practiceCategoryRepository = practiceCategoryRepository;
    }

    public async ValueTask<Result<GetProjectPracticeCategoriesQueryOut>> Handle(GetProjectPracticeCategoriesQuery query, CancellationToken ct)
    {
        var result = Result<GetProjectPracticeCategoriesQueryOut>.Success();

        var project = await _projectRepository.Get(query.ProjectID, result);
        var practiceCategoryRoot = await _practiceCategoryRepository.Get(project.PracticeCategoryRootID, result);

        var @out = new GetProjectPracticeCategoriesQueryOut(
           new PracticeCategoriesListVM() { PracticeCategoriesRootID = practiceCategoryRoot.ID });

        var practiceCategoriesIDs = practiceCategoryRoot.ChildrenIDs;
        if (practiceCategoriesIDs.IsNullOrEmpty())
            return result.With(@out);

        var practiceCategories = await _practiceCategoryRepository.Get(practiceCategoriesIDs, result);
        practiceCategories = practiceCategories.OrderBy(s => practiceCategoriesIDs.IndexOf(s.ID)).ToArray();

        @out = new GetProjectPracticeCategoriesQueryOut(
            new PracticeCategoriesListVM() 
            {
                PracticeCategoriesRootID = practiceCategoryRoot.ID,
                Values = practiceCategories.Select(s => new PracticeCategoryVM()
                {
                    Identity = (s.ID, s.Name),
                })
                .ToArray()
            });

        return result.With(@out);
    }
}

public sealed record GetProjectPracticeCategoriesQuery(string ProjectID) : IQuery<Result<GetProjectPracticeCategoriesQueryOut>>;
public sealed record GetProjectPracticeCategoriesQueryOut(PracticeCategoriesListVM SignatureList);

public class PracticeCategoriesListVM
{
    public string PracticeCategoriesRootID { get; set; } = "";
    public PracticeCategoryVM[] Values { get; set; } = Array.Empty<PracticeCategoryVM>();
}

public class PracticeCategoryVM
{
    public IdentityVM Identity { get; set; } = new();
}
