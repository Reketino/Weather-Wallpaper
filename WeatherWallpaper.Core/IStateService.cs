using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Core;

public interface IStateService
{
    Task<WallpaperState?> LoadAsync();
    
}