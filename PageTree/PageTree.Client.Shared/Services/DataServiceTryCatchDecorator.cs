namespace PageTree.Client.Shared.Services
{
    public class DataServiceTryCatchDecorator<TException> : IDataService
        where TException : Exception
    {
        private readonly IDataService _decorated;
        private readonly Action<TException> _onCatch;

        public DataServiceTryCatchDecorator(IDataService decorated, Action<TException> onCatch)
        {
            _decorated = decorated;
            _onCatch = onCatch;
        }

        public async Task<TResponse> Get<TResponse>(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _decorated.Get<TResponse>(cancellationToken);
            }
            catch (TException exception)
            {
                _onCatch.Invoke(exception);
                return default;
            }
        }
    }
}
