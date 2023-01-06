namespace PageTree.Client.Shared.Interfaces
{
    public class RefreshInvoker
    {
        public Func<Task> Action { set; private get; }

        public Task Invoke()
        {
            if (Action != null)
                return Action.Invoke();

            return Task.CompletedTask;
        }
    }
}
