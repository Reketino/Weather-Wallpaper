using System.Text.Json;
using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Infrastructure;

public sealed class IpLocationService : ILocationService
{
    private readonly HttpClient _http = new();

    public async Task<LocationData> GetLocationAsync()
    {
        var json = await _http.GetStringAsync(
            "https://ipwho.is/"
        );

        using var document = JsonDocument.Parse(json);

        return new LocationData
        {
            City = document.RootElement
                .GetProperty("city")
                .GetString() ?? "Unknown",

            Latitude = document.RootElement
                    .GetProperty("latitude")
                    .GetDouble(),

            Longitude = document.RootElement
                    .GetProperty("longitude")
                    .GetDouble()
        };
    }
}