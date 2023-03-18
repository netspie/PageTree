using PageTree.LegacyDataMigrator;

var resultPrinter = new ResultPrinter();

var migrator = new Migrator()
{
    _legacyDataAccessor = new LegacyDataAccessor(),

    _dataTransformer = new DataTransformer()
    {
        _resultPrinter = resultPrinter,
    },

    _dataStorage = new DataStorage()
    {
        _resultPrinter = resultPrinter,
    },
};

await migrator.Perform();
