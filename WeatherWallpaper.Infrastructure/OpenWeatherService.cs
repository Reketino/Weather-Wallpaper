using System.Text.Json;
using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Infrastructure;

public class OpenWeatherService : IWeatherService
{
    private readonly HttpClient _http = new();
    
    public async Task<WeatherData> GetWeatherAsync()
    {
        var url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=62.38&lon=6.44";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("User-Agent", "weather-wallpaper-app"); 

        var data = JsonDocument.Parse(json); 

        var timeseries = data.RootElement
            .GetProperty("properties")
            .GetProperty("timeseries")[0];

        var instant = timeseries
            .GetProperty("data")
    }

}
