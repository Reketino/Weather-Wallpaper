using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;
using WeatherWallpaper.Infrastructure;
using WeatherWallpaper.Infrastructure.Wallpapers;

namespace WeatherWallpaper.Tray;

internal static class Program
{
    private static readonly SemaphoreSlim _semaphore = new (1, 1);

    private static string? _lastCondition;

    [STAThread]
    static async Task Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var config = ConfigLoader.Load();

        ILocationService locationService = new IpLocationService();
        var location = await locationService.GetLocationAsync();
        Console.WriteLine($" {location.City}");

        var weatherService = new MetWeatherService(
            location.Latitude,
            location.Longitude
        );
        var wallpaperService = new WindowsWallpaperService();
        var wallpaperProvider = new LocalWallpaperProvider();

        var tray = new NotifyIcon()
        {
            Icon = IconMapper.GetIcon("Clear"),
            Visible = true,
            Text = "Weather Wallpaper"
        };

        var menu = new ContextMenuStrip();
        menu.Items.Add("Update now", null, async (s, e) =>
        {
           await SafeUpdate(weatherService, wallpaperService, wallpaperProvider, tray); 
        });

        menu.Items.Add("Exit", null, (s, e) =>
        {
            tray.Visible = false;
            Application.Exit();
        });

        tray.ContextMenuStrip = menu;

        var timer = new System.Windows.Forms.Timer
        {
            Interval = config.Wallpaper.UpdateIntervalMinutes * 60 * 1000
        };

        timer.Tick += async (s, e) =>
        {
            await SafeUpdate(weatherService, wallpaperService, wallpaperProvider, tray);
        };

        timer.Start();

        _ = SafeUpdate(weatherService, wallpaperService, wallpaperProvider, tray);

        Application.Run();
    }

    private static async Task SafeUpdate(
        IWeatherService weatherService,
        IWallpaperService wallpaperService,
        IWallpaperProvider wallpaperProvider,
        NotifyIcon tray)
    {
        if (!await _semaphore.WaitAsync(0))
        return;

        try
        {
            var weather = await weatherService.GetWeatherAsync();

            tray.Icon = IconMapper.GetIcon(weather.Condition);
            tray.Text = $"Weather: {weather.Condition} | {weather.Temperature:F1}°C";

            if (_lastCondition == weather.Condition)
            {
                Console.WriteLine($"Skipping wallpaper upd8 ({weather.Condition})");

                return;
            }
            _lastCondition = weather.Condition;

            var image =  await wallpaperProvider.GetWallpaperAsync(weather.Condition);

            wallpaperService.SetWallpaper(image);

            Console.WriteLine($"Updated: {weather.Condition} | {weather.Temperature:F1}°C");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}