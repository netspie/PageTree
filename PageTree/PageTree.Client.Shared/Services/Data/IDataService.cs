namespace PageTree.Client.Shared.Services.Data
{
    public interface IDataService
    {
        Task<TResponse> Get<TResponse>(CancellationToken cancellationToken = default);
    }
}
