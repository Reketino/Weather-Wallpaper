using System.Runtime.InteropServices;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure;

public class WindowsWallpaperService : IWallpaperService
{
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINFILE = 0X01;
    private const int SPIF_SENDCHANGE = 0X02;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(
        int uAction,
        int uParam,
        string lpvParam,
        int fuWinINI);

    public void SetWallpaper(string relativePath)
    {
        var fullPath = Path.GetFullPath(Path.Combine("Assets", relativePath));

        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"[Wallpaper] File not found: {fullPath}");
        }
    }

}