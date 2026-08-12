using WeatherWallpaper.Core;
using WeatherWallpaper.Domain;
using WeatherWallpaper.Infrastructure;
using WeatherWallpaper.Infrastructure.Wallpapers;
using WeatherWallpaper.Tray.Forms;

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

        IStateService stateService = new StateService();
        
        var state = await stateService.LoadAsync();
        if (state is not null)
        {
            _lastCondition = state.LastCondition;

            Console.WriteLine(
                $"Loaded state: {_lastCondition}");
            
        }

        ILocationService locationService = new IpLocationService();
        var location = await locationService.GetLocationAsync();
        Console.WriteLine($" {location.City} ({location.Latitude:F4}, {location.Longitude:F4})");

        IWeatherService weatherService = new MetWeatherService(
            location.Latitude,
            location.Longitude
        );
        IWallpaperService wallpaperService = new WindowsWallpaperService();
        IWallpaperProvider wallpaperProvider = new LocalWallpaperProvider();

        var tray = new NotifyIcon()
        {
            Icon = IconMapper.GetIcon("Clear"),
            Visible = true,
            Text = "Weather Wallpaper"
        };

        var menu = new ContextMenuStrip();
        menu.Items.Add("Update now", null, async (s, e) =>
        {
           await SafeUpdate(weatherService, wallpaperService, wallpaperProvider, stateService, tray); 
        });

        menu.Items.Add("Settings...", null, (s, e) =>
        {
            using var form = new SettingsForm();

            form.ShowDialog();
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
            await SafeUpdate(weatherService, wallpaperService, wallpaperProvider, stateService, tray);
        };

        timer.Start();

        _ = SafeUpdate(weatherService, wallpaperService, wallpaperProvider,stateService, tray);

        Application.Run();
    }

    private static async Task SafeUpdate(
        IWeatherService weatherService,
        IWallpaperService wallpaperService,
        IWallpaperProvider wallpaperProvider,
        IStateService stateService,
        NotifyIcon tray)
    {
        if (!await _semaphore.WaitAsync(0))
        return;

        try
        {
            var state = await stateService.LoadAsync();
            var weather = await weatherService.GetWeatherAsync();

            tray.Icon = IconMapper.GetIcon(weather.Condition);
            tray.Text = $"Weather: {weather.Condition} | {weather.Temperature:F1}°C";

            if (_lastCondition == weather.Condition)
            {
                Console.WriteLine($"Skipping wallpaper upd8 ({weather.Condition})");

                return;
            }
            _lastCondition = weather.Condition;

            var image =  await wallpaperProvider.GetWallpaperAsync(
                weather.Condition,
                state?.LastWallpaper
                );

            wallpaperService.SetWallpaper(image);

            await stateService.SaveAsync(
                new WallpaperState
                {
                    LastCondition = weather.Condition,
                    LastWallpaper = Path.GetFileName(image)
                }
            );

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