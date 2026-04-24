using WeatherWallpaper.Core;
using WeatherWallpaper.Infrastructure;

Console.WriteLine("Weather Wallpaper starting...");

IWeatherService weatherService = new MetWeatherService();
IWallpaperService wallpaperService = new WindowsWallpaperService();
var selector = new BackgroundSelector();
