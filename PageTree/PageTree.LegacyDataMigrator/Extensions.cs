using Common.Basic.DDD;
using Common.Basic.Repository;
using Corelibs.Basic.Repository;
using Data;

namespace PageTree.LegacyDataMigrator
{
    public static class Extensions
    {
        public static IRepository<TEntity> GetJsonDbRepository<TEntity, TDataEntity>(AppDbContext dbContext, string tableName)
            where TEntity : class, IEntity
            where TDataEntity : JsonEntity, new()
        {
            var dbContextRP = new DbContextRepository<TDataEntity>(dbContext);
            return new JsonEntityRepositoryDecorator<TEntity, TDataEntity>(dbContextRP);
        }
    }
}
