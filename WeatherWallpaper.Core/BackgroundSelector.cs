namespace WeatherWallpaper.Core;

public class BackgroundSelector
{
    public string Select(string condition)
    {
        return condition switch
        {
            "Rain" => "rain.jpg",
            "Clear" => "sunny.jpg",
            "Snow" => "snow.jpg",
            _ => "default.jpg"
        };
    }
}