using System.Text.Json;
using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;

namespace WeatherWallpaper.Infrastructure;

public sealed class StateService : IStateService
{
    private readonly string _path = 
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
        "WeatherWallpaper", 
        "state.json");
    public async Task<WallpaperState?> LoadAsync()
    {
        
    }
}