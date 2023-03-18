extern alias CommonLegacy;
extern alias PracticerLegacy;

using Common.Basic.Blocks;
using Page = PracticerLegacy::Practicer.Domain.Pages.Content.Page;
using Signature = PracticerLegacy::Practicer.Domain.Templates.TemplateSignature;

namespace PageTree.LegacyDataMigrator;

public class LegacyDataAccessor
{
    private readonly CommonLegacy::Common.Basic.Repository.IRepository<Page> _pageRepository;
    private readonly CommonLegacy::Common.Basic.Repository.IRepository<Signature> _signatureRepository;

    public LegacyDataAccessor() 
    {
        var dirOps = new CommonLegacy::Common.Basic.Files.DirectoryOperations();
        var fileOps = new CommonLegacy::Common.Basic.Files.FileOperations();
        var jsonConverter = new CommonLegacy::Common.Basic.Json.NewtonsoftJsonConverter();

        var basePath = @"C:\Users\dariu\AppData\LocalLow\daretsuki\Practicer";

        _pageRepository = new CommonLegacy::Common.Basic.Repository.LocalStorageRepository<Page>(
            @$"{basePath}\Pages",
            dirOps, fileOps, jsonConverter);

        _signatureRepository = new CommonLegacy::Common.Basic.Repository.LocalStorageRepository<Signature>(
           @$"{basePath}\TemplateSignatures",
           dirOps, fileOps, jsonConverter);
    }

    public async Task<Result<LegacyData>> Get(DataType dataTypes)
    {
        var result = Result<LegacyData>.Success();

        var pages = await GetEntities(_pageRepository, dataTypes, DataType.Page, result);
        var signatures = await GetEntities(_signatureRepository, dataTypes, DataType.Signature, result);

        var resultDTO = new LegacyData(
            pages,
            signatures);

        return Result<LegacyData>.Success(resultDTO).With(result);
    }

    private static async Task<T[]> GetEntities<T>(
        CommonLegacy::Common.Basic.Repository.IRepository<T> repository,
        DataType dataTypes,
        DataType dataType,
        Result result)
    {
        if (!dataTypes.HasFlag(dataType))
            return Array.Empty<T>();

        var getResult = await repository.GetAll();
        result += getResult;
        if (!getResult.IsSuccess)
            return Array.Empty<T>();

        return getResult.Get();
    }
}
