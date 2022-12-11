using System.Net.Http.Json;

namespace PageTree.Client.Shared.Services
{
    public class HttpDataService : IDataService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpDataService(IHttpClientFactory clientFactory) 
        {
            _clientFactory = clientFactory;
        }

        public async Task<TResponse> Get<TResponse>(CancellationToken cancellationToken = default)
        {
            var type = typeof(TResponse);
            var name = type.IsArray ? type.Name.Remove(type.Name.Length - 2, 2) : type.Name;

            try
            {
                var client = _clientFactory.CreateClient("authorized");
                return await client.GetFromJsonAsync<TResponse>($"{name}", cancellationToken);
            }
            catch (Exception ex)
            {
                var client = _clientFactory.CreateClient("anonymous");
                return await client.GetFromJsonAsync<TResponse>($"{name}", cancellationToken);
            }
        }
    }
}
