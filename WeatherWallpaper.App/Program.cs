using WeatherWallpaper.Core;
using WeatherWallpaper.Infrastructure;
using WeatherWallpaper.Infrastructure.Wallpapers;

Console.WriteLine("Weather Wallpaper starting...");

IWeatherService weatherService = new MetWeatherService();
IWallpaperService wallpaperService = new WindowsWallpaperService();
IWallpaperProvider wallpaperProvider = new LocalWallpaperProvider();
var selector = new BackgroundSelector();


while (true)
{
    await RunOnce(weatherService, wallpaperService, wallpaperProvider);

    Console.WriteLine("We are waiting 30 mins for engine to start running🏃🏻‍➡️...\n");
    await Task.Delay(TimeSpan.FromMinutes(10));
}

static async Task RunOnce(
    IWeatherService weatherService,
    IWallpaperService wallpaperService,
    BackgroundSelector selector)
{
    try
    {
        var weather = await weatherService.GetWeatherAsync();
        Console.WriteLine($"{weather.Temperature}°C | {weather.Condition}");

        var image = BackgroundSelector.Select(weather.Condition);
        wallpaperService.SetWallpaper(image);
        Console.WriteLine($"Wallpaper changed: {image}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error Mayday: {ex.Message}");
    }
}
