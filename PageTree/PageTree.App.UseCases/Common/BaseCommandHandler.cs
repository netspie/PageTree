namespace PageTree.App.UseCases.Common
{
    public abstract class BaseCommandHandler
    {
        protected string NewID => Guid.NewGuid().ToString();
    }
}
