using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure.Wallpapers;

public class LocalWallpaperProvider : IWallpaperProvider
{
  private readonly string _basePath = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Assests")
  );

  private readonly Random _random = new();  

  public Task<string> GetWallpaperAsync(string condition)
    {
        var folder = condition switch
        {
            "Rain" => "Rain",
            "Snow" => "Snow",
            "Clouds" => "Clouds",
            _ => "Clear"
        };

        var path = Path.Combine(_basePath, folder);
        
        Console.WriteLine($"[DEBUG] Checking folder: {path}");
    }
}