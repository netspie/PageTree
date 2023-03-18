using Common.Basic.Blocks;

namespace PageTree.LegacyDataMigrator;

public class DataTransformer
{
    //public required Action< _resultPrinter { private get; init; }
    public required Printer _printer { private get; init; }

    public async Task<Result<PageTreeData>> Transform(LegacyData legacyData)
    {
        return Result<PageTreeData>.Failure();
    }
}
