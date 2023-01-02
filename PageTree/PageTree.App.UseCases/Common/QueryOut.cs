namespace PageTree.App.UseCases.Common
{
    public abstract record QueryOut
    {
        public static implicit operator bool(QueryOut @out) => @out != null;
    }
}
