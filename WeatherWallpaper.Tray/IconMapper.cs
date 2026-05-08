namespace WeatherWallpaper.Tray;

public static class IconMapper
{

  public static Icon GetIcon(string condition)
    {
        var basePath = Path.Combine(AppContext.BaseDirectory, "Assets");

        var file = condition switch
        {
          "Rain" => "rain.ico",
          "Snow" => "snow.ico",
          "Clouds" => "cloud.ico",
          _ => "sun.ico",

        };

        var path = Path.Combine(basePath, file);

        return File.Exists(path)
        ? new Icon(path)
        : SystemIcons.Application;
    }  
}