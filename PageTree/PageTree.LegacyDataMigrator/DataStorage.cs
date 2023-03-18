using Common.Basic.Blocks;

namespace PageTree.LegacyDataMigrator;

public class DataStorage
{
    public required Printer _printer { private get; init; }

    public async Task<Result> Store(PageTreeData data)
    {
        return Result.Failure();
    }
}
