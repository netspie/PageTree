using PageTree.App.Entities.Signatures;
using PageTree.Domain;

namespace PageTree.LegacyDataMigrator;

public record PageTreeData(
    Page[] Pages = null,
    Signature[] Signatures = null);