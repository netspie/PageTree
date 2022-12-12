namespace PageTree.Client.Shared.Services
{
    public interface IAuthUser
    {
        Task SignIn();
        Task SignOut();

        Task<bool> IsSignedIn();
        string Name { get; }

        event Action<bool> OnAuthenticatedStateChanged;
    }
}
