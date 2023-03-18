using Common.Basic.Blocks;

namespace PageTree.LegacyDataMigrator;

public class DataTransformer
{
    public required ResultPrinter _resultPrinter { private get; init; }

    public async Task<Result<PageTreeData>> Transform(LegacyData legacyData)
    {
        return Result<PageTreeData>.Failure();
    }
}
