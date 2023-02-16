using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.App.Entities.Styles;
using PageTree.Domain.Projects;

namespace PageTree.App.UseCases.Signatures.Queries;

public class GetSignaturesQueryHandler : IQueryHandler<GetSignaturesQuery, Result<GetSignaturesQueryOut>>
{
    private IRepository<Project> _projectRepository;
    private IRepository<Signature> _signatureRepository;
    private IRepository<Style> _styleRepository;

    public GetSignaturesQueryHandler(
        IRepository<Project> projectRepository, 
        IRepository<Signature> signatureRepository,
        IRepository<Style> styleRepository)
    {
        _projectRepository = projectRepository;
        _signatureRepository = signatureRepository;
        _styleRepository = styleRepository;
    }

    public async ValueTask<Result<GetSignaturesQueryOut>> Handle(GetSignaturesQuery query, CancellationToken ct)
    {
        var result = Result<GetSignaturesQueryOut>.Success();

        var project = await _projectRepository.Get(query.projectID, result);
        var signatureRoot = await _signatureRepository.Get(project.SignatureRootID, result);

        var signatureIDs = signatureRoot.ChildrenIDs;
        var signatures = await _signatureRepository.Get(signatureIDs, result);

        var signatureInfos = await Task.WhenAll(
            signatures.Select(async s =>
            {
                var styles = await _styleRepository.Get(s.StyleIDs, result);
                if (styles == null)
                    styles = Array.Empty<Style>();

                return new { signature = s, styles = styles };
            })
            .ToArray());

        var @out = new GetSignaturesQueryOut(
            new SignatureListVM() 
            {
                Values = signatureInfos.Select(s => new SignatureVM()
                {
                    Identity = new()
                    {
                        ID = s.signature.ID,
                        Name = s.signature.Name,
                    },

                    Styles = s.styles.Select(style => new IdentityVM()
                    {
                        ID = style.ID,
                        Name = style.Name,
                    })
                    .ToArray()

                })
                .ToArray()
            });

        return result.With(@out);
    }
}

public sealed record GetSignaturesQuery(string projectID) : IQuery<Result<GetSignaturesQueryOut>>;
public sealed record GetSignaturesQueryOut(SignatureListVM SignatureList);

public class SignatureListVM
{
    public SignatureVM[] Values { get; set; } = Array.Empty<SignatureVM>();
}

public class SignatureVM
{
    public IdentityVM Identity { get; set; } = new();
    public IdentityVM[] Styles { get; set; } = Array.Empty<IdentityVM>();
}

public class IdentityVM
{
    public string ID { get; set; } = "";
    public string Name { get; set; } = "";
}
