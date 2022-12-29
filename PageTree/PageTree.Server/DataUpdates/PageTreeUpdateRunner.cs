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
                var updates = types.Select(t => ActivatorUtilities.CreateInstance(scope.ServiceProvider, t)).Cast<IDataUpdater>().ToArray();
                foreach (var update in updates)
                    await update.Update();
            }
        }
    }
}
