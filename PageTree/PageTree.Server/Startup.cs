using BuildingBlocks.Repository;
using Common.Basic.Repository;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Domain.Users;
using Practicer.Domain.Practice;
using Corelibs.Basic.Repository;
using PageTree.Server.Data;
using Corelibs.BlazorShared;
using Common.Basic.DDD;
using System.Linq.Expressions;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace PageTree.Server.Api
{
    public static class Startup
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddJsonDbRepository<Domain.Users.User, PageTree.Server.Data.User>(nameof(AppDbContext.Users));

            services.AddSingleton<IRepository<Page>>(sp =>
               new WWWRootHttpClientRepository<Page>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<Signature>>(sp =>
               new WWWRootHttpClientRepository<Signature>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<PracticeCategory>>(sp =>
               new WWWRootHttpClientRepository<PracticeCategory>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<PracticeTactic>>(sp =>
               new WWWRootHttpClientRepository<PracticeTactic>(sp.GetRequiredService<HttpClient>(), "", ""));
        }

        private static void AddJsonDbRepository<TEntity, TDataEntity>(this IServiceCollection services, string tableName)
            where TEntity : class, IEntity
            where TDataEntity : JsonEntity, new()
        {
            services.AddSingleton<IRepository<TEntity>>(sp =>
            {
                var dbContext = sp.Get<IDbContextFactory<AppDbContext>>().CreateDbContext();
                var dbContextRP = new DbContextRepository<TDataEntity>(dbContext);
                return new JsonEntityRepositoryDecorator<TEntity, TDataEntity>(dbContextRP);
            });
        }
    }
}
