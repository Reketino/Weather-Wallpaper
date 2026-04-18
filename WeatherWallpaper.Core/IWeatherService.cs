using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Core;

public interface IWeatherService
{
    Task<WeatherData> GetWeatherAsync();
}
