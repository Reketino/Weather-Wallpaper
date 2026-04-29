using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeatherWallpaper.Core;
using WeatherWallpaper.Infrastructure;
using WeatherWallpaper.Infrastructure.Wallpapers;

namespace WeatherWallpaper.Tray;

internal static class Program
{
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
    }
}