using Common.Basic.Blocks;
using Mediator;

namespace PageTree.App.Pages.Queries;

public sealed record GetPageOfIDQuery(string ID) : IQuery<Result<GetPageOfIDQueryDTO>>;

public sealed record GetPageOfIDQueryDTO(PageVM PageVM);

public class PageVM
{
    public IdentityVM[] Path { get; init; } = Array.Empty<IdentityVM>();
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; init; } = Array.Empty<PropertyVM>();
    public IdentityVM[] PracticeTactics { get; init; } = Array.Empty<IdentityVM>();
}

public class PropertyVM
{
    public IdentityVM Identity { get; init; } = new IdentityVM();
    public IdentityVM SignatureIdentity { get; init; } = new IdentityVM();
    public PropertyVM[] Properties { get; init; } = Array.Empty<PropertyVM>();
}

public class IdentityVM
{
    public string ID { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}