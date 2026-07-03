using System.Text.Json;
using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;
using System.Globalization;

namespace WeatherWallpaper.Infrastructure;

public class MetWeatherService : IWeatherService
{
    private readonly HttpClient _http = new();
    private readonly double _latitude;
    private readonly double _longitude;

    public MetWeatherService(double latitude, double longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }
    
    public async Task<WeatherData> GetWeatherAsync()
    {
        var latitude = _latitude.ToString(CultureInfo.InvariantCulture);
        var longitude = _longitude.ToString(CultureInfo.InvariantCulture);

        var url = 
            $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={latitude}&lon={longitude}";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("User-Agent", "weather-wallpaper-app"); 

        var response = await _http.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonDocument.Parse(json); 

        var timeseries = data.RootElement
            .GetProperty("properties")
            .GetProperty("timeseries")[0];

        var instant = timeseries
            .GetProperty("data")
            .GetProperty("instant")
            .GetProperty("details");

        var temp = instant
            .GetProperty("air_temperature")
            .GetDouble();

        var symbol = timeseries
            .GetProperty("data")
            .GetProperty("next_1_hours")
            .GetProperty("summary")
            .GetProperty("symbol_code")
            .GetString();

        return new WeatherData
        {
            Temperature = temp,
            Condition = MapSymbolToCondition(symbol!)
        };
    }

    private static string MapSymbolToCondition(string symbol)
    {
        if (symbol.Contains("rain"))
            return "Rain";
        
         if (symbol.Contains("snow"))
            return "Snow";

        if (symbol.Contains("cloud"))
            return "Clouds";

        return "Clear";
    }

}
