using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Core;

public interface ILocationService
{
    Task<LocationData> GetLocationAsync();
}