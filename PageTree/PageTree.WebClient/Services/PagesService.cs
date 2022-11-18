using PageTree.App.Pages.Queries;
using PageTree.App.Practice.Queries;
using System.Net.Http.Json;
using System.Text.Json;

namespace PageTree.WebClient.Services
{
    public class PagesService : IPagesService
    {
        private readonly HttpClient _httpClient;

        public PagesService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<PageVM> GetPage(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = "dsdsd";

            // should be repo.. cached data..
            var str = await _httpClient.GetStringAsync($"pages?id={id}");
            var json = await _httpClient.GetFromJsonAsync<GetPageOfIDQueryDTO>($"pages?id={id}");
            Console.WriteLine(str);
            //var stream = await _httpClient.GetStreamAsync($"pages?id={id}");
            //var dto = await JsonSerializer.DeserializeAsync<GetPageOfIDQueryDTO>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });

            return new PageVM();
        }

        public Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID)
        {
            return Task.FromResult(Array.Empty<PracticeCardDTO>());
        } 
    }
}
