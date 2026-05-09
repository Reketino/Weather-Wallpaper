using WeatherWallpaper.Core;
using WeatherWallpaper.Infrastructure;
using WeatherWallpaper.Infrastructure.Wallpapers;

namespace WeatherWallpaper.Tray;

internal static class Program
{
    private static readonly SemaphoreSlim _semaphore = new (1, 1);

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var weatherService = new MetWeatherService();
        var wallpaperService = new WindowsWallpaperService();
        var wallpaperProvider = new LocalWallpaperProvider();

        var tray = new NotifyIcon()
        {
            Icon = SystemIcons.Application,
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
            Interval = 10 * 60 * 1000
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
            var image =  await wallpaperProvider.GetWallpaperAsync(weather.Condition);

            wallpaperService.SetWallpaper(image);

            Console.WriteLine($"Updated: {weather.Condition}");
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