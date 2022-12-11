namespace PageTree.Client.Shared.Services
{
    public interface IDataService
    {
        Task<TResponse> Get<TResponse>(CancellationToken cancellationToken = default);
    }
}
