using Common.Basic.Blocks;

namespace PageTree.LegacyDataMigrator;

public class LegacyDataAccessor
{
    public async Task<Result<LegacyData>> Get(DataType dataTypes)
    {
        return Result<LegacyData>.Failure();
    }
}
