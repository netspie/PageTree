using PageTree.LegacyDataMigrator;

var printer = new Printer();

var migrator = new Migrator()
{
    _legacyDataAccessor = new LegacyDataAccessor(),

    _dataTransformer = new DataTransformer()
    {
        _printer = printer,
    },

    _dataStorage = new DataStorage()
    {
        _printer = printer,
    },

    _printer = printer,
};

await migrator.Perform(DataType.Page | DataType.Signature);
