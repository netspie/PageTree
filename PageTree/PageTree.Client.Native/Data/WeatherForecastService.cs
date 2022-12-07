using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace PageTree.Client.Native.Data;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigation;

    public WeatherForecastService(HttpClient httpClient, NavigationManager navigation)
    {
        _httpClient = httpClient;
        _navigation = navigation;
    }

    // Call the Secure Web API.
    public async Task<List<WeatherForecast>> CallSecureWebApi()
    {
        var result = new List<WeatherForecast>();

        // Get the response.
        var responseString = await _httpClient.GetStringAsync($"{_navigation?.BaseUri}/weatherforecast");
        if (responseString == string.Empty)
            return result;

        // Serialize the response.
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        result = JsonSerializer.Deserialize<List<WeatherForecast>>(responseString, options);

        return result!;
    }
}