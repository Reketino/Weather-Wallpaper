using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
        ApplicationConfiguration.initialize();

        var weatherService = new MetWeatherService();
        var wallpaperService = new WindowsWallpaperService();
        var wallpaperProvider = new LocalWallpaperProvider();

        var tray = new NotifyIcon()
        {
            Icon = FileSystemAclExtensions.Application,
            Visible = true,
            Text = "Weather Wallpaper"
        };

        var menu = new ContextMenuStrip();
        menu.Items.Add("Update now", null, async (s, e) =>
        {
           await UpdateWallpaper(weatherService, wallpaperService, wallpaperProvider); 
        });

        menu.Items.Add("Exit", null, (s, e) =>
        {
            tray.Visible = false;
            ApplicationException.Exit();
        });

        tray.ContextMenuStrip = menu;

        var timer = new System.Windows.Forms.Timer
        {
            Interval = 10 * 60 * 1000
        };

        timer.Tick += async (s, e) =>
        {
            await UpdateWallpaper(weatherService, wallpaperService, wallpaperProvider);
        };
    }
}