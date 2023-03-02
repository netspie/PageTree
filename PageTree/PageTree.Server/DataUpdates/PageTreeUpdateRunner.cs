using Common.Basic.Blocks;
using PageTree.Server.Data;

namespace PageTree.Server.DataUpdates
{
    public static class PageTreeUpdateRunner
    {
        public static async Task UpdateData(this IServiceProvider services)
        {
            var type = typeof(IDataUpdater);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract && !p.IsInterface)
                .ToArray();

            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var updates = types.Select(t => ActivatorUtilities.CreateInstance(
                            scope.ServiceProvider, t)).Cast<IDataUpdater>().ToArray();

                        var result = Result.Success();
                        foreach (var update in updates)
                            result.Add(await update.Update());
                        
                        if (result.IsSuccess)
                            await transaction.CommitAsync();

                        Console.WriteLine("Data update result: " + result.IsSuccess);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
