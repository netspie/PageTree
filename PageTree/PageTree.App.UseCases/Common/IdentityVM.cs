namespace PageTree.App.UseCases.Common;

public class IdentityVM
{
    public string ID { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public IdentityVM() { }
    public IdentityVM(string id) => ID = id;
    public IdentityVM(string id, string name) : this(id) => Name = name ?? string.Empty;

    public static implicit operator IdentityVM((string id, string name) args) => new IdentityVM(args.id, args.name);
}
