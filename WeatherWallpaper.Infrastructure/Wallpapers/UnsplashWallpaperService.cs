using System.Text.Json;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure.Wallpapers;

public class UnsplashWallpaperService : IWallpaperProvider
{
    private readonly HttpClient _http = new();
}