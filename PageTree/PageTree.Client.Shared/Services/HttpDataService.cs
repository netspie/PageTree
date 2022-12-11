using System.Net.Http.Json;

namespace PageTree.Client.Shared.Services
{
    public class HttpDataService : IDataService
    {
        private readonly HttpClient _client;

        public HttpDataService(HttpClient client) 
        {
            _client = client;
        }

        public async Task<TResponse> Get<TResponse>(CancellationToken cancellationToken = default)
        {
            var type = typeof(TResponse);
            var name = type.IsArray ? type.Name.Remove(type.Name.Length - 2, 2) : type.Name;
            var response = await _client.GetFromJsonAsync<TResponse>($"{name}", cancellationToken);

            return response;
        }
    }
}
