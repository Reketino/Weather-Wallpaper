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

        Console.WriteLine($"Loading icon: {path}");

        if (!File.Exists(path))
    {
      Console.WriteLine("Icon not found going to fallback");
      return SystemIcons.Application;
    }
    var icon = new Icon(path);

    Console.WriteLine( $"Icon size: {icon.Size.Width}x{icon.Size.Height}");

    return icon;
    }  
}