using BuildingBlocks.Repository;
using Common.Basic.DDD;
using Common.Basic.Repository;
using Corelibs.Basic.Repository;
using Corelibs.BlazorShared;
using Corelibs.MongoDB;
using Corelibs.MongoDB.Logging;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PageTree.App.UseCases;
using PageTree.Domain.Practice;
using PageTree.Server.Authorization;
using PageTree.Server.Data;
using PageTree.Server.DataUpdates;
using Practicer.Domain.Practice;

namespace PageTree.Server.Api
{
    public static class Startup
    {
        public static void AddPageTreeSetup(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddMediatorExt();
            services.AddMongoDbLogger(env);

            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DbContextTransactionBehaviour<,>));

            services.AddRepositories();
            services.AddResourceAuthorizationHandlers();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<DataUpdate>, DbContextRepository<DataUpdate>>();

            services.AddJsonDbRepository<Domain.Users.User, PageTree.Server.Data.User>(nameof(AppDbContext.Users));
            services.AddJsonDbRepository<Domain.Projects.ProjectUserList, PageTree.Server.Data.ProjectUserList>(nameof(AppDbContext.ProjectUserLists));
            services.AddJsonDbRepository<Domain.Projects.Project, PageTree.Server.Data.Project>(nameof(AppDbContext.Projects));
            services.AddJsonDbRepository<Domain.Page, PageTree.Server.Data.Page>(nameof(AppDbContext.Pages));
            services.AddJsonDbRepository<Domain.Signature, PageTree.Server.Data.Signature>(nameof(AppDbContext.Signatures));

            services.AddSingleton<IRepository<PracticeCategory>>(sp =>
               new WWWRootHttpClientRepository<PracticeCategory>(sp.GetRequiredService<HttpClient>(), "", ""));

            services.AddSingleton<IRepository<PracticeTactic>>(sp =>
               new WWWRootHttpClientRepository<PracticeTactic>(sp.GetRequiredService<HttpClient>(), "", ""));
        }

        private static void AddResourceAuthorizationHandlers(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.Edit, policy =>
                    policy.Requirements.Add(new SameAuthorRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, ResourceAuthorizationHandler<Domain.Projects.ProjectUserList>>();
            services.AddSingleton<IAuthorizationHandler, ResourceAuthorizationHandler<Domain.Projects.Project>>();

        }

        private static void AddJsonDbRepository<TEntity, TDataEntity>(this IServiceCollection services, string tableName)
            where TEntity : class, IEntity
            where TDataEntity : JsonEntity, new()
        {
            services.AddScoped<IRepository<TEntity>>(sp =>
            {
                var dbContext = sp.Get<AppDbContext>();
                var dbContextRP = new DbContextRepository<TDataEntity>(dbContext);
                return new JsonEntityRepositoryDecorator<TEntity, TDataEntity>(dbContextRP);
            });
        }

        private static void AddMongoDbLogger(this IServiceCollection services, IWebHostEnvironment env)
        {
            var conn = Environment.GetEnvironmentVariable("LogDatabaseConn");
            var databaseName = env.IsDevelopment() ? "pageTree_log_dev" : "pageTree_log_prod";

            var logger = new MongoDbLogger(new MongoDbRepositoryT<Log>(conn, databaseName, "logs"));
            services.AddSingleton<Corelibs.Basic.Logging.ILogger>(logger);
            logger.Log("Start server");
        }
    }
}
