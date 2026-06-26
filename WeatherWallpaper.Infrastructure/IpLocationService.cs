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
            "https://ipapi.co/json"
        );

        using var document = JsonDocument.Parse(json);
    }
}