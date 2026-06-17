namespace WeatherWallpaper.Domain;

public sealed class LocationData
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string City { get; set; } = "";
}