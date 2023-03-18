using Common.Basic.Blocks;

namespace PageTree.LegacyDataMigrator;

internal class Migrator
{
    public required LegacyDataAccessor _legacyDataAccessor { private get; init; }
    public required DataTransformer _dataTransformer { private get; init; }
    public required DataStorage _dataStorage { private get; init; }

    public async Task<Result> Perform(DataType dataTypes = DataType.None)
    {
        var legacyDataResult = await _legacyDataAccessor.Get(dataTypes);
        if (!legacyDataResult.IsSuccess)
            return legacyDataResult;

        var dataTransformResult = await _dataTransformer.Transform(legacyDataResult.Get());
        if (!dataTransformResult.IsSuccess)
            return dataTransformResult;

      return await _dataStorage.Store(dataTransformResult.Get());
    }
}
