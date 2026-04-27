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

        if (!Directory.Exists(path))
        {
            Console.WriteLine($"[WallpaperProvider] Folder not found: {path}");
            return Task.FromResult(Path.Combine(_basePath, "default.jpg"));
        }

        var files = Directory.GetFiles(path);

        Console.WriteLine($"[DEBUG] Files found: {files.Length}");

        if (files.Length == 0)
        {
            return Task.FromResult(Path.Combine(_basePath, "default.jpg"));
        }

        var selected = files[_random.Next(files.Length)];

        Console.WriteLine($"[DEBUG] Selected: {selected}");
    }
}