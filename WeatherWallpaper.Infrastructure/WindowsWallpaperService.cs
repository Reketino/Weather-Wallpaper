using System.Runtime.InteropServices;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure;

public partial class WindowsWallpaperService : IWallpaperService
{
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0X01;
    private const int SPIF_SENDCHANGE = 0X02;

    [LibraryImport("user32.dll", SetLastError = true)]
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

        var result = SystemParametersInfo(
            SPI_SETDESKWALLPAPER,
            0,
            fullPath,
            SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
        );

        if (!result)
        {
            var error = Marshal.GetLastWin32Error();
            Console.WriteLine($"[Wallpaper] Failed to set wallpaper. Error: {error}");
        }
        else
        {
            Console.WriteLine($"[Wallpaper] Updated: {relativePath}");
        }
    }

}