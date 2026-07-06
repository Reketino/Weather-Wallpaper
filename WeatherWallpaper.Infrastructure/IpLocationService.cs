using System.Text.Json;
using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Infrastructure;

public sealed class IpLocationService : ILocationService
{
    private readonly HttpClient _http = new();

    public async Task<LocationData> GetLocationAsync()
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "https://ipwho.is/"
        );

        request.Headers.UserAgent.ParseAdd("WeatherWallpaper/1.0");

        var response = await _http.SendAsync(request);
        Console.WriteLine($"Location API:{(int)response.StatusCode} {response.StatusCode}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

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