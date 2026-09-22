namespace WeatherWallpaper.Core;

public sealed class AppSettings
{
   public WeatherSettings Weather { get; set; } = new(); 
   public WallpaperSettings Wallpaper { get; set; } = new();
}

public sealed class WeatherSettings
{
   public bool AutomaticLocation { get; set; } = true;
   public double Latitude { get; set; } = 62.38;
   public double Longitude { get; set; } = 6.44;
    
}

public sealed class WallpaperSettings
{
   public int UpdateIntervalMinutes { get; set; } = 10;
}