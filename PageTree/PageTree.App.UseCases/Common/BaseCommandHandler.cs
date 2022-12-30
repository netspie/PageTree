namespace PageTree.App.UseCases.Common
{
    public abstract class BaseCommandHandler
    {
        protected static string NewID => Guid.NewGuid().ToString();
    }
}
