using Data;
using PageTree.LegacyDataMigrator;

var dbContext = new AppDbContext();
var pageRepository = Extensions.GetJsonDbRepository<PageTree.Domain.Page, Page>(dbContext, nameof(AppDbContext.Pages));

var migrator = new Migrator()
{
    _legacyDataAccessor = new LegacyDataAccessor(),
    _dataTransformer = new DataTransformer()
    {
        ProjectID = "8e6458d5-ab4a-4dbd-b845-7727c5b87c9c",
        RootPageID = "350b10d0-f72a-4f8f-92a6-5da3b80cac6a",
        LegacyRootPageID = "0e018409-c777-46bc-bd03-345466ab8292",
        LegacyRootPageParentID = "Root-BFADA6FD-C9A6-42F9-874C-D8C1BCE0A2CD",
        OwnerID = "d958f1dd-734f-4e55-bf2a-d344e1c62b5c",
        _pageRepository = pageRepository
    },
    _dataStorage = new DataStorage(),
};

await migrator.Perform(DataType.Page | DataType.Signature);
