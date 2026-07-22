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
        if (!File.Exists(_path))
        return null;

        var json = await File.ReadAllTextAsync(_path);

        return JsonSerializer.Deserialize<WallpaperState>(json);
    }

    public async Task SaveAsync(WallpaperState state)
    {
        var folder = Path.GetDirectoryName(_path)!;
    }
}