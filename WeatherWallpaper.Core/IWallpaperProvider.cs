namespace WeatherWallpaper.Core;

public interface IWallpaperProvider
{
    Task<string> GetWallpaperAsync(string conditon);
}