namespace PageTree.Client.Shared.Services
{
    public interface IAuthUser
    {
        Task SignIn();
        Task SignOut();

        bool IsSignedIn { get; }
        string Name { get; }

        event Action<bool> OnAuthenticatedStateChanged;
    }
}
