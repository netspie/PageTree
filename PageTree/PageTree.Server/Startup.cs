using BuildingBlocks.Repository;
using Common.Basic.DDD;
using Common.Basic.Repository;
using Corelibs.Basic.Repository;
using Corelibs.BlazorShared;
using Mediator;
using Microsoft.EntityFrameworkCore;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Server.Data;
using Practicer.Domain.Practice;

namespace PageTree.Server.Api
{
    public static class Startup
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<DbContext>(sp =>
                sp.Get<IDbContextFactory<AppDbContext>>().CreateDbContext());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DbContextTransactionBehaviour<,>));

            services.AddJsonDbRepository<Domain.Users.User,                 PageTree.Server.Data.User>              (nameof(AppDbContext.Users));
            services.AddJsonDbRepository<Domain.Projects.ProjectUserList,   PageTree.Server.Data.ProjectUserList>   (nameof(AppDbContext.ProjectUserLists));
            services.AddJsonDbRepository<Domain.Projects.Project,           PageTree.Server.Data.Project>           (nameof(AppDbContext.Projects));
            services.AddJsonDbRepository<Domain.Page,                       PageTree.Server.Data.Page>              (nameof(AppDbContext.Pages));
            services.AddJsonDbRepository<Domain.Signature,                  PageTree.Server.Data.Signature>         (nameof(AppDbContext.Signatures));

            services.AddSingleton<IRepository<PracticeCategory>>(sp =>
               new WWWRootHttpClientRepository<PracticeCategory>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<PracticeTactic>>(sp =>
               new WWWRootHttpClientRepository<PracticeTactic>(sp.GetRequiredService<HttpClient>(), "", ""));
        }

        private static void AddJsonDbRepository<TEntity, TDataEntity>(this IServiceCollection services, string tableName)
            where TEntity : class, IEntity
            where TDataEntity : JsonEntity, new()
        {
            services.AddScoped<IRepository<TEntity>>(sp =>
            {
                var dbContext = sp.Get<IDbContextFactory<AppDbContext>>().CreateDbContext();
                var dbContextRP = new DbContextRepository<TDataEntity>(dbContext);
                return new JsonEntityRepositoryDecorator<TEntity, TDataEntity>(dbContextRP);
            });
        }
    }
}
