using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Core;

public interface IStateService
{
    Task<WallpaperState?> LoadAsync();
    Task SaveAsync(WallpaperState state);
}