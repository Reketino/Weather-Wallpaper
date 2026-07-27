namespace WeatherWallpaper.Domain;

public sealed class WallpaperState
{
    public string LastCondition { get; set; } = "";
    public string LastWallpaper { get; set; } = "";
}