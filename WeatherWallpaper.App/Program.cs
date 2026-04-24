using WeatherWallpaper.Core;
using WeatherWallpaper.Infrastructure;

Console.WriteLine("Weather Wallpaper starting...");

IWeatherService weatherService = new MetWeatherService();
IWallpaperService wallpaperService = new WindowsWallpaperService();
var selector = new BackgroundSelector();

while (true)
{
    await RunOnce(weatherService, wallpaperService, selector);

    Console.WriteLine("We are waiting 30 mins for engine to start running🏃🏻‍➡️...\n");
    await Task.Delay(TimeSpan.FromMinutes(30));
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
        
    }
}
