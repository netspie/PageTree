namespace PageTree.Client.Shared.Auth
{
    public interface ISignInRedirector
    {
        void Redirect(Exception exception);
    }
}
