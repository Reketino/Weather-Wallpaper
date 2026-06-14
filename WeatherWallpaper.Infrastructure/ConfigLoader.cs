using System.Text.Json;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure;

public static class ConfigLoader
{
    public static AppSettings Load()
    {
       var json = File.ReadAllText("appsettings.json");

       return JsonSerializer.Deserialize<AppSettings>(json)
            ?? new AppSettings(); 
    }
}