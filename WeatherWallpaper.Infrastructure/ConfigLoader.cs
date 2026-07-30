using System.Text.Json;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure;

public static class ConfigLoader
{
    public static AppSettings Load()
    {
       var path = Path.Combine(
        AppContext.BaseDirectory,
        "appsettings.json");

        var json = File.ReadAllText(path);

       return JsonSerializer.Deserialize<AppSettings>(json)
            ?? new AppSettings(); 
    }
}