using System.Text.Json;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure;

public static class ConfigLoader
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };
    public static AppSettings Load()
    {
       var path = Path.Combine(
        AppContext.BaseDirectory,
        "appsettings.json");

        var json = File.ReadAllText(path);

       return JsonSerializer.Deserialize<AppSettings>(json)
            ?? new AppSettings(); 
    }

    public static void Save(AppSettings config)
    {
        var path = Path.Combine(
            AppContext.BaseDirectory,
            "appsettings.json"
        );
    }
}