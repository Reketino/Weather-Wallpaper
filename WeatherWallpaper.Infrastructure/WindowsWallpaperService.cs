using System.Runtime.InteropServices;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Infrastructure;

public class WindowsWallpaperService : IWallpaperService
{
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINFILE = 0X01;
    private const int SPIF_SENDCHANGE = 0X02;

}