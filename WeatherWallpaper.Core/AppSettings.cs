namespace WeatherWallpaper.Core;

public sealed class AppSettings
{
   public WeatherSettings Weather { get; set; } = new(); 
}

public sealed class WeatherSettings
{
   public double Latitude { get; set; }
   public double Longitude { get; set; }
    
}

public sealed class WallpaperSettings
{
   
}