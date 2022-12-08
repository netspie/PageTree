using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace PageTree.Client.Native.Data;

public interface IWeatherForecastService
{
    Task<List<WeatherForecast>> CallSecureWebApi();
}

public class WeatherForecastService : IWeatherForecastService
{
    private  HttpClient _httpClient;

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Call the Secure Web API.
    public async Task<List<WeatherForecast>> CallSecureWebApi()
    {
        var result = new List<WeatherForecast>();

        // Get the response.
        var responseString = await _httpClient.GetStringAsync($"/pages?id=123");
        if (responseString == string.Empty)
            return result;

        return result!;
    }
}